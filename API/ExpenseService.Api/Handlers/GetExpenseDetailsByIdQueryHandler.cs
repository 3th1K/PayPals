using System.Security.Claims;
using Common.DTOs.ExpenseDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using ExpenseService.Api.Queries;
using MediatR;

namespace ExpenseService.Api.Handlers
{
    public class GetExpenseDetailsByIdQueryHandler : IRequestHandler<GetExpenseDetailsByIdQuery, ApiResult<ExpenseResponse>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetExpenseDetailsByIdQueryHandler(IExpenseRepository expenseRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _expenseRepository = expenseRepository;   
        }
        public async Task<ApiResult<ExpenseResponse>> Handle(GetExpenseDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetExpenseDetails(request.Id);
            if (expense == null)
            {
                return ApiResult<ExpenseResponse>.Failure(ErrorType.ErrExpenseNotFound, "Expense Was Not Found");
            }

            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            if (
                authenticatedUserRole != "Admin" &&
                authenticatedUserId  != expense.PayerId.ToString() &&
                expense.Users.SingleOrDefault(u => u.UserId.ToString().Equals(authenticatedUserId)) == null
            )
            {
                return ApiResult<ExpenseResponse>.Failure(ErrorType.ErrUserForbidden, "User is not Authorized To Access This Content");
            }
            return ApiResult<ExpenseResponse>.Success(expense);
        }
    }
}
