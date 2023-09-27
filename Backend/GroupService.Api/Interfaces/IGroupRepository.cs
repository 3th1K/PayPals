using Data.DTOs.ExpenseDTOs;
using Data.DTOs.GroupDTOs;
using Data.Models;

namespace GroupService.Api.Interfaces
{
    public interface IGroupRepository
    {
        public Task<List<GroupResponse>> GetAllGroups();
        public Task<GroupResponse> CreateGroup(Group group);
        public Task<GroupResponse> DeleteGroup(int id);
        public Task<GroupResponse> UpdateGroup(GroupUpdateRequest groupUpdateRequest);
        public Task<GroupResponse> AddMemberInGroup(int groupId, int userId);
        public Task<GroupResponse> DeleteMemberFromGroup(int groupId, int userId);
        public Task<GroupResponse> GetGroupById(int id);
        public Task<Group?> GetGroupByName(string name);
        public Task<List<ExpenseResponse>> GetGroupExpensesById(int id);
        public Task<bool> CheckUserExistenceInGroup(int groupId, int userId);
    }
}
