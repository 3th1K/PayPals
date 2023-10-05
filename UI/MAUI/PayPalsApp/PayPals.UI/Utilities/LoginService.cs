using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using PayPals.UI.DTOs;
using PayPals.UI.Interfaces;

namespace PayPals.UI.Utilities
{
    public class LoginService : ILoginService
    {
        private readonly IRestService _restService;
        public LoginService(IRestService restService)
        {
            _restService = restService;
        }

        public async Task<ApiResult<string>> DoLogin(LoginRequest request)
        {
            Uri uri = new Uri(string.Format(_restService.RestUrl + "/login"));
            try
            {
                //var json = JsonSerializer.Serialize<LoginRequest>(request, _restService.SerializerOptions);
                //var content = new StringContent(json, Encoding.UTF8, "application/json");

                var serializedData = _restService.Serializer<LoginRequest>(request);
                HttpResponseMessage response = await _restService.Client.PostAsync(uri, serializedData);
                
                //HttpResponseMessage response = await _restService.Client.PostAsync(uri, content);
                //var token = await response.Content.ReadAsStringAsync();
                //var hey = JsonSerializer.Deserialize<Error>(token, _restService.SerializerOptions);
                if (response.IsSuccessStatusCode)
                {
                    var deserializedData = await _restService.Deserializer<string>(response);
                    return ApiResult<string>.Success(deserializedData);
                }
                else
                {
                    var deserializedData = await _restService.Deserializer<Error>(response);
                    return ApiResult<string>.Failure(deserializedData);
                }
            }
            catch (Exception ex)
            {
                //do something
                return ApiResult<string>.Failure(new Error(){ErrorDescription = ex.Message});
            }

        }
    }
}
