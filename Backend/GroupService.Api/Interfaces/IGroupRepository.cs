using GroupService.Api.Models;

namespace GroupService.Api.Interfaces
{
    public interface IGroupRepository
    {
        public Task<GroupResponse> GetGroupById(int id);
        public Task<List<ExpenseResponse>> GetGroupExpensesById(int id);
        public Task<bool> CheckUserExistenceInGroup(int groupId, int userId);
    }
}
