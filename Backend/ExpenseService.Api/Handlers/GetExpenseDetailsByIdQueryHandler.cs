using ExpenseService.Api.Exceptions;
using ExpenseService.Api.Interfaces;
using ExpenseService.Api.Models;
using ExpenseService.Api.Queries;
using MediatR;
using System.Security.Claims;

namespace ExpenseService.Api.Handlers
{
    public class GetExpenseDetailsByIdQueryHandler : IRequestHandler<GetExpenseDetailsByIdQuery, ExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetExpenseDetailsByIdQueryHandler(IExpenseRepository expenseRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _expenseRepository = expenseRepository;   
        }
        public async Task<ExpenseResponse> Handle(GetExpenseDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetExpenseDetails(request.Id)?? throw new ExpenseNotFoundException();
            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            if (
                authenticatedUserRole != "Admin" &&
                authenticatedUserId  != expense.PayerId.ToString() &&
                expense.Users.SingleOrDefault(u => u.UserId.ToString().Equals(authenticatedUserId)) == null
            )
            {
                throw new UserNotAuthorizedException();
            }
            return expense;
        }
    }
}
