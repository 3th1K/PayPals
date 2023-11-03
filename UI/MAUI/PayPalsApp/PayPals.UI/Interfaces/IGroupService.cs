using PayPals.UI.DTOs.GroupDTOs;
using PayPals.UI.Utilities;

namespace PayPals.UI.Interfaces
{
    public interface IGroupService
    {
        Task<ApiResult<GroupResponse>> GetGroupDetailsAsync(int id);
        Task<ApiResult<GroupResponse>> CreateGroupAsync(GroupRequest request);
        Task<ApiResult<GroupResponse>> AddGroupMemberAsync(int id, GroupMember member);
    }
}
