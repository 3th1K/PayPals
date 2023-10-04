using Common.DTOs.ExpenseDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using ExpenseService.Api.Queries;
using MediatR;

namespace ExpenseService.Api.Handlers
{
    public class DeleteExpenseByIdQueryHandler : IRequestHandler<DeleteExpenseByIdQuery, ApiResult<ExpenseResponse>>
    {
        private readonly IExpenseRepository _expenseRepository;
        public DeleteExpenseByIdQueryHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<ApiResult<ExpenseResponse>> Handle(DeleteExpenseByIdQuery request,
            CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.DeleteExpense(request.Id);
            if (expense == null)
            {
                return ApiResult<ExpenseResponse>.Failure(ErrorType.ErrExpenseNotFound, "Expense with this Id does not exists");
            }

            return ApiResult<ExpenseResponse>.Success(expense);
        }
    }
}
