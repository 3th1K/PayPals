using MediatR;
using System.Security.Claims;
using Common.DTOs.ExpenseDTOs;
using Common.Exceptions;
using Common.Interfaces;

namespace ExpenseService.Api.Handlers
{
    public class ExpenseUpdateRequestHandler : IRequestHandler<ExpenseUpdateRequest, ExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExpenseUpdateRequestHandler(IExpenseRepository expenseRepository, IHttpContextAccessor httpContextAccessor)
        {
            _expenseRepository = expenseRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ExpenseResponse> Handle(ExpenseUpdateRequest request, CancellationToken cancellationToken)
        {
            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            var expense = await _expenseRepository.GetExpenseDetails(request.ExpenseId) ??
                          throw new ExpenseNotFoundException("Expense Does Not Exists");

            if (authenticatedUserRole != "Admin" && authenticatedUserId != expense.PayerId.ToString())
            {
                throw new UserForbiddenException("User is not allowed to update this expense");
            }

            var updatedExpense = await _expenseRepository.UpdateExpense(request);
            return updatedExpense;
        }
    }
}
