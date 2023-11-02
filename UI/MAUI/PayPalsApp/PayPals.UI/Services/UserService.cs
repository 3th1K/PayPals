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
    public class UserService : IUserService
    {
        private readonly IRestService _restService;
        private readonly IStorageService _storageService;
        public UserService(IRestService restService, IStorageService storageService)
        {
            _restService = restService;
            _storageService = storageService;
        }

        public async Task<ApiResult<UserDetailsResponse>> GetUserDetailsAsync(int id)
        {
            Uri uri = new Uri(string.Format(_restService.RestUrl + "/users/details/" + id));

            try
            {
                _restService.Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await _storageService.GetTokenAsync());
                HttpResponseMessage response = await _restService.Client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var deserializedData = await _restService.Deserializer<UserDetailsResponse>(response);
                    return ApiResult<UserDetailsResponse>.Success(deserializedData);
                }
                else
                {
                    var deserializedData = await _restService.Deserializer<Error>(response);
                    return ApiResult<UserDetailsResponse>.Failure(deserializedData);
                }
            }
            catch(Exception ex)
            {
                return ApiResult<UserDetailsResponse>.Failure(new Error() { ErrorDescription = ex.Message });
            }
        }

        public async Task<ApiResult<List<GroupResponse>>> GetUserGroupsAsync(int id)
        {
            Uri uri = new Uri(string.Format($"{_restService.RestUrl}/users/{id}/groups"));
            try
            {
                _restService.Client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await _storageService.GetTokenAsync());
                HttpResponseMessage response = await _restService.Client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var deserializedData = await _restService.Deserializer<List<GroupResponse>>(response);
                    return ApiResult<List<GroupResponse>>.Success(deserializedData);
                }
                else
                {
                    var deserializedData = await _restService.Deserializer<Error>(response);
                    return ApiResult<List<GroupResponse>>.Failure(deserializedData);
                }
            }
            catch (Exception ex)
            {
                return ApiResult<List<GroupResponse>>.Failure(new Error() { ErrorDescription = ex.Message });
            }
        }
    }
}
