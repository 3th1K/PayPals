using ExpenseService.Api.Models;

namespace ExpenseService.Api.Interfaces
{
    public interface IExpenseRepository
    {
        public Task<ExpenseResponse> CreateExpense(ExpenseRequest request);
        public Task<Expense> GetExpenseDetails(int id);
    }
}
