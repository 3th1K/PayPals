using Common.DTOs.ExpenseDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using MediatR;

namespace ExpenseService.Api.Handlers
{
    public class ExpenseApprovalRequestHandler : IRequestHandler<ExpenseApprovalRequest, ApiResult<ExpenseResponse>>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserRepository _userRepository;
        public ExpenseApprovalRequestHandler(IExpenseRepository expenseRepository, IUserRepository userRepository)
        {
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
        }
        public async Task<ApiResult<ExpenseResponse>> Handle(ExpenseApprovalRequest request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetExpenseDetails(request.ExpenseId);
            if (expense == null)
            {
                return ApiResult<ExpenseResponse>.Failure(ErrorType.ErrExpenseNotFound,
                    $"Expense with {request.ExpenseId} is not present");
            }

            var user = await _userRepository.GetUserById(request.UserId);
            if (user == null)
            {
                return ApiResult<ExpenseResponse>.Failure(ErrorType.ErrUserNotFound,
                    "User in the approve request is not a valid user");
            }

            var existingApproval = expense.ExpenseApprovals.FirstOrDefault(ea => ea.UserId == request.UserId);
            if (existingApproval != null)
            {
                return ApiResult<ExpenseResponse>.Failure(ErrorType.ErrApprovalAlreadyExists,
                    "User already given the approval for this expense");
            }

            var expenseResponse = await _expenseRepository.SubmitExpenseApproval(request);
            return ApiResult<ExpenseResponse>.Success(expenseResponse);
        }
    }
}
