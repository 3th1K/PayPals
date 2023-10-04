using Common.DTOs.ExpenseDTOs;

namespace Common.Interfaces
{
    public interface IExpenseRepository
    {
        public Task<List<ExpenseResponse>> GetAll();
        public Task<ExpenseResponse> CreateExpense(ExpenseRequest request);
        public Task<ExpenseResponse> UpdateExpense(ExpenseUpdateRequest request);
        public Task<ExpenseResponse> DeleteExpense(int id);
        public Task<ExpenseResponse> GetExpenseDetails(int id);
        public Task<ExpenseResponse> SubmitExpenseApproval(ExpenseApprovalRequest request);
        public Task<ExpenseApprovalResponse> GetExpenseApprovals(int id);
        public Task<ExpenseStatusResponse> GetExpenseStatus(int id);
        public Task<ExpenseResponse> AddParticipant(int expenseId, int userId);

        public Task<ExpenseResponse> DeleteParticipant(int expenseId, int userId);
    }
}
