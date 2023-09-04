using ExpenseService.Api.Exceptions;
using ExpenseService.Api.Interfaces;
using ExpenseService.Api.Models;
using ExpenseService.Api.Queries;
using MediatR;

namespace ExpenseService.Api.Handlers
{
    public class GetExpenseDetailsByIdQueryHandler : IRequestHandler<GetExpenseDetailsByIdQuery, Expense>
    {
        private readonly IExpenseRepository _expenseRepository;
        public GetExpenseDetailsByIdQueryHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;   
        }
        public async Task<Expense> Handle(GetExpenseDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetExpenseDetails(request.Id)?? throw new ExpenseNotFoundException();
            return expense;
        }
    }
}
