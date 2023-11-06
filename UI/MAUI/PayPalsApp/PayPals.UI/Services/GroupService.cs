using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PayPals.UI.DTOs;
using PayPals.UI.DTOs.GroupDTOs;
using PayPals.UI.DTOs.UserDTOs;
using PayPals.UI.Interfaces;
using PayPals.UI.Utilities;

namespace PayPals.UI.Services
{
    public class GroupService : IGroupService
    {
        private readonly IRestService _restService;
        private readonly IStorageService _storageService;

        public GroupService(IRestService restService, IStorageService storageService)
        {
            _restService = restService;
            _storageService = storageService;
        }

        public async Task<ApiResult<GroupResponse>> GetGroupDetailsAsync(int id)
        {
            Uri uri = new Uri(string.Format(_restService.RestUrl + "/groups/" + id));

            try
            {
                _restService.Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await _storageService.GetTokenAsync());
                HttpResponseMessage response = await _restService.Client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var deserializedData = await _restService.Deserializer<GroupResponse>(response);
                    return ApiResult<GroupResponse>.Success(deserializedData);
                }
                else
                {
                    var deserializedData = await _restService.Deserializer<Error>(response);
                    return ApiResult<GroupResponse>.Failure(deserializedData);
                }
            }
            catch (Exception ex)
            {
                return ApiResult<GroupResponse>.Failure(new Error() { ErrorDescription = ex.Message });
            }
        }

        public async Task<ApiResult<GroupResponse>> CreateGroupAsync(GroupRequest request)
        {
            Uri uri = new Uri(string.Format(_restService.RestUrl + "/groups/create"));

            try
            {
                _restService.Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await _storageService.GetTokenAsync());

                var serializedRequest = _restService.Serializer<GroupRequest>(request);
                HttpResponseMessage response = await _restService.Client.PostAsync(uri, serializedRequest);

                if (response.IsSuccessStatusCode)
                {
                    var deserializedData = await _restService.Deserializer<GroupResponse>(response);
                    return ApiResult<GroupResponse>.Success(deserializedData);
                }
                else
                {
                    var deserializedData = await _restService.Deserializer<Error>(response);
                    return ApiResult<GroupResponse>.Failure(deserializedData);
                }
            }
            catch (Exception ex)
            {
                return ApiResult<GroupResponse>.Failure(new Error() { ErrorDescription = ex.Message });
            }
        }

        public async Task<ApiResult<GroupResponse>> AddGroupMemberAsync(int id, GroupMember member)
        {
            Uri uri = new Uri(string.Format($"{_restService.RestUrl}/groups/{id}/member"));
            try
            {
                _restService.Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await _storageService.GetTokenAsync());

                var serializedRequest = _restService.Serializer<GroupMember>(member);
                HttpResponseMessage response = await _restService.Client.PutAsync(uri, serializedRequest);

                if (response.IsSuccessStatusCode)
                {
                    var deserializedData = await _restService.Deserializer<GroupResponse>(response);
                    return ApiResult<GroupResponse>.Success(deserializedData);
                }
                else
                {
                    var deserializedData = await _restService.Deserializer<Error>(response);
                    return ApiResult<GroupResponse>.Failure(deserializedData);
                }
            }
            catch (Exception ex)
            {
                return ApiResult<GroupResponse>.Failure(new Error() { ErrorDescription = ex.Message });
            }
        }
    }
}
