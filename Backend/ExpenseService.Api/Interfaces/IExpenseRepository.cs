using Data.Models;
using ExpenseService.Api.Models;

namespace ExpenseService.Api.Interfaces
{
    public interface IExpenseRepository
    {
        public Task<List<ExpenseResponse>> GetAll();
        public Task<ExpenseResponse> CreateExpense(ExpenseRequest request);
        public Task<ExpenseResponse> DeleteExpense(int id);
        public Task<ExpenseResponse> GetExpenseDetails(int id);
    }
}
