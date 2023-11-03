using PayPals.UI.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using PayPals.UI.DTOs.GroupDTOs;
using PayPals.UI.DTOs.UserDTOs;

namespace PayPals.UI.Services
{
    public class StorageService : IStorageService
    {
        public const string TokenKey = "AuthToken";
        public const string UserKey = "AuthUser";
        public const string UserGroupsKey = "AuthUserGroups";

        public enum StorageResult
        {
            Success,
            NotFound,
            ValidationFailure,
            Failure
        }

        private readonly IRestService _restService;
        public StorageService(IRestService restService)
        {
            _restService = restService;
        }
        public async Task<string> GetTokenAsync()
        {
            var token = await SecureStorage.GetAsync(TokenKey);
            if (token == null) 
            {
                return null;
            }
            if (!ValidateToken(token))
            { 
                await RemoveTokenAsync();
                return null;
            }
            return token;
        }

        public async Task<UserDetailsResponse> GetUserAsync()
        {
            var user = await SecureStorage.GetAsync(UserKey);
            if (user == null) 
            {
                return null;
            }
            return _restService.StorageDataDeserializer<UserDetailsResponse>(user);
        }

        public Task RemoveTokenAsync()
        {
            SecureStorage.Remove(TokenKey);
            return Task.CompletedTask;
        }

        public Task RemoveUserAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> ExtractUserIdFromToken()
        {
            var token = await GetTokenAsync();
            if (ValidateToken(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "userId");

                return int.Parse(userIdClaim.Value);
            }
            else
            {
                return -1;
            }
        }

        public async Task SetTokenAsync(string token)
        {
            if (ValidateToken(token))
            {
                try
                {
                    await SecureStorage.SetAsync(TokenKey, token);
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex.Message}");
                }
            }
            else
            {
                throw new Exception("Error while setting the token");
            }
        }

        public async Task SetUserAsync(UserDetailsResponse user)
        {
            var serializedUser = _restService.StorageDataSerializer<UserDetailsResponse>(user);
            await SecureStorage.SetAsync(UserKey, serializedUser);
        }

        private bool ValidateToken(string token)
        {
            // Perform token validation logic here, e.g., using JWT libraries.
            // Return true if the token is valid; otherwise, return false.
            // You can customize this method as per your token format and validation requirements.

            // For example, you can use a library like System.IdentityModel.Tokens.Jwt to validate JWT tokens.
            // Replace this with actual token validation code.
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                // Check if the token is expired
                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    return false; // Token is expired
                }

                // Add more validation checks as needed.

                return true;
            }
            catch
            {
                return false; // Token is invalid
            }
        }

        public async Task SetUserGroupsAsync(List<GroupResponse> userGroups)
        {
            var serializedUserGroups = _restService.StorageDataSerializer<List<GroupResponse>>(userGroups);
            await SecureStorage.SetAsync(UserGroupsKey, serializedUserGroups);
        }

        public async Task AddUserGroupAsync(GroupResponse group)
        {
            var deserializedUserGroups = await GetUserGroupsAsync();
            if (deserializedUserGroups == null)
            {
                throw new Exception();
            }
            deserializedUserGroups.Add(group);
            var serializedUserGroups = _restService.StorageDataSerializer<List<GroupResponse>>(deserializedUserGroups);
            await SecureStorage.SetAsync(UserGroupsKey, serializedUserGroups);
        }

        public async Task<List<GroupResponse>> GetUserGroupsAsync()
        {
            var userGroups = await SecureStorage.GetAsync(UserGroupsKey);
            if (userGroups == null)
            {
                return null;
            }
            else
            {
                return _restService.StorageDataDeserializer<List<GroupResponse>>(userGroups);
            }
        }

        public Task RemoveAllDataAsync()
        {
            SecureStorage.RemoveAll();
            return Task.CompletedTask;
        }
    }
}
