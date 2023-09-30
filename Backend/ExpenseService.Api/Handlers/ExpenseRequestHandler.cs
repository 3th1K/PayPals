using System.Security.Claims;
using Common.DTOs.ExpenseDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using MediatR;

namespace ExpenseService.Api.Handlers
{
    public class ExpenseRequestHandler : IRequestHandler<ExpenseRequest, ApiResult<ExpenseResponse>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExpenseRequestHandler(IExpenseRepository expenseRepository, IHttpContextAccessor httpContextAccessor)
        {
            _expenseRepository = expenseRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<ExpenseResponse>> Handle(ExpenseRequest request, CancellationToken cancellationToken)
        {
            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            if (
                authenticatedUserRole != "Admin" &&
                request.PayerId.ToString() != authenticatedUserId
            )
            {
                return ApiResult<ExpenseResponse>.Failure(ErrorType.ErrUserForbidden, "User Is Not Authorized To Access This Content");
            }
            var expenseResponse = await _expenseRepository.CreateExpense(request);
            return ApiResult<ExpenseResponse>.Success(expenseResponse);


        }
    }
}
