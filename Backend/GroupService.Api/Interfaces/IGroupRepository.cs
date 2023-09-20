using Data.Models;
using GroupService.Api.Models;

namespace GroupService.Api.Interfaces
{
    public interface IGroupRepository
    {
        public Task<GroupResponse> CreateGroup(Group group);
        public Task<GroupResponse> GetGroupById(int id);
        public Task<Group?> GetGroupByName(string name);
        public Task<List<ExpenseResponse>> GetGroupExpensesById(int id);
        public Task<bool> CheckUserExistenceInGroup(int groupId, int userId);
    }
}
