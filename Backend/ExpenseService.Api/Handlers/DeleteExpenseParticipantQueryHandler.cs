using System.Security.Claims;
using Common.DTOs.ExpenseDTOs;
using Common.Interfaces;
using Common.Utilities;
using ExpenseService.Api.Queries;
using MediatR;

namespace ExpenseService.Api.Handlers;

public class DeleteExpenseParticipantQueryHandler : IRequestHandler<DeleteExpenseParticipantQuery, ApiResult<ExpenseResponse>>
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IHttpContextAccessor _contextAccessor;
    public DeleteExpenseParticipantQueryHandler(IExpenseRepository expenseRepository, IUserRepository userRepository, IHttpContextAccessor contextAccessor)
    {
        _expenseRepository = expenseRepository;
        _userRepository = userRepository;
        _contextAccessor = contextAccessor;
    }
    public async Task<ApiResult<ExpenseResponse>> Handle(DeleteExpenseParticipantQuery request, CancellationToken cancellationToken)
    {
        var expense = await _expenseRepository.GetExpenseDetails(request.ExpenseId);
        if (expense == null)
        {
            return ApiResult<ExpenseResponse>.Failure(ErrorType.ErrExpenseNotFound, "Invalid Expense");
        }

        var authenticatedUserId = _contextAccessor.HttpContext?.User.FindFirstValue("userId");
        var authenticatedUserRole = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

        if (authenticatedUserRole != "Admin" && authenticatedUserId != expense.PayerId.ToString())
        {
            return ApiResult<ExpenseResponse>.Failure(ErrorType.ErrUserForbidden,
                "User do not have rights to delete participants from this expense");
        }

        var user = await _userRepository.GetUserById(request.UserId);
        if (user == null)
        {
            return ApiResult<ExpenseResponse>.Failure(ErrorType.ErrUserNotFound, "Provided user in the request is not a valid user");
        }

        var isUserAlreadyInExpense = expense.Users.Any(u => u.UserId == request.UserId);
        if (!isUserAlreadyInExpense)
        {
            return ApiResult<ExpenseResponse>.Failure(ErrorType.ErrUserAlreadyExists, "Provided user in the request does not exists in the expense");
        }

        var updatedExpense = await _expenseRepository.DeleteParticipant(request.ExpenseId, request.UserId);
        return ApiResult<ExpenseResponse>.Success(updatedExpense);

    }
}