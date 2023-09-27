using System.Security.Claims;
using Common.Exceptions;
using Data.DTOs.ExpenseDTOs;
using ExpenseService.Api.Interfaces;
using MediatR;

namespace ExpenseService.Api.Handlers
{
    public class ExpenseRequestHandler : IRequestHandler<ExpenseRequest, ExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExpenseRequestHandler(IExpenseRepository expenseRepository, IHttpContextAccessor httpContextAccessor)
        {
            _expenseRepository = expenseRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ExpenseResponse> Handle(ExpenseRequest request, CancellationToken cancellationToken)
        {
            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            if (
                authenticatedUserRole != "Admin" &&
                request.PayerId.ToString() != authenticatedUserId
            )
            {
                throw new UserForbiddenException("User Is Not Authorized To Access This Content");
            }
            var expenseResponse = await _expenseRepository.CreateExpense(request);
            return expenseResponse;


        }
    }
}
