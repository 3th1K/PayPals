using Common.DTOs.ExpenseDTOs;
using Common.Interfaces;
using Common.Utilities;
using ExpenseService.Api.Queries;
using MediatR;

namespace ExpenseService.Api.Handlers
{
    public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQuery, ApiResult<List<ExpenseResponse>>>
    {
        private readonly IExpenseRepository _expenseRepository;
        public GetAllExpensesQueryHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        public async Task<ApiResult<List<ExpenseResponse>>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
        {
            var expenses = await _expenseRepository.GetAll();

            return ApiResult<List<ExpenseResponse>>.Success(expenses);
        }
    }
}
