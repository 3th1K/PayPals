using PayPals.UI.DTOs.GroupDTOs;
using PayPals.UI.DTOs.UserDTOs;
using PayPals.UI.Services;

namespace PayPals.UI.Interfaces
{
    public interface IStorageService
    {
        Task SetTokenAsync(string token);
        Task<string> GetTokenAsync();
        Task RemoveTokenAsync();


        Task SetUserAsync(UserDetailsResponse user);
        Task<UserDetailsResponse> GetUserAsync();
        Task RemoveUserAsync();
        Task<int> ExtractUserIdFromToken();


        Task SetUserGroupsAsync(List<GroupResponse> userGroups);
        Task<List<GroupResponse>> GetUserGroupsAsync();

        Task RemoveAllDataAsync();

    }
}
