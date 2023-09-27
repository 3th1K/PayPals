using Common.DTOs.ExpenseDTOs;
using Common.Interfaces;
using ExpenseService.Api.Queries;
using MediatR;

namespace ExpenseService.Api.Handlers
{
    public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQuery, List<ExpenseResponse>>
    {
        private readonly IExpenseRepository _expenseRepository;
        public GetAllExpensesQueryHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        public async Task<List<ExpenseResponse>> Handle(GetAllExpensesQuery request, CancellationToken cancellationToken)
        {
            return await _expenseRepository.GetAll();
        }
    }
}
