using Common.DTOs.ExpenseDTOs;
using Common.Interfaces;
using Common.Utilities;
using ExpenseService.Api.Queries;
using MediatR;
using System.Security.Claims;

namespace ExpenseService.Api.Handlers;

public class GetExpenseStatusByIdQueryHandler : IRequestHandler<GetExpenseStatusByIdQuery, ApiResult<ExpenseStatusResponse>>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public GetExpenseStatusByIdQueryHandler(IExpenseRepository expenseRepository, IHttpContextAccessor httpContextAccessor)
    {
        _expenseRepository = expenseRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResult<ExpenseStatusResponse>> Handle(GetExpenseStatusByIdQuery request, CancellationToken cancellationToken)
    {
        var expense = await _expenseRepository.GetExpenseDetails(request.Id);
        if (expense == null)
        {
            return ApiResult<ExpenseStatusResponse>.Failure(ErrorType.ErrExpenseNotFound, "Expense Was Not Found");
        }
        var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
        var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
        if (
            authenticatedUserRole != "Admin" &&
            authenticatedUserId != expense.PayerId.ToString() &&
            expense.Users.SingleOrDefault(u => u.UserId.ToString().Equals(authenticatedUserId)) == null
        )
        {
            return ApiResult<ExpenseStatusResponse>.Failure(ErrorType.ErrUserForbidden, "User is not Authorized To Access This Content");
        }

        var expenseStatus = await _expenseRepository.GetExpenseStatus(expense.ExpenseId);
        return ApiResult<ExpenseStatusResponse>.Success(expenseStatus);
    }
}