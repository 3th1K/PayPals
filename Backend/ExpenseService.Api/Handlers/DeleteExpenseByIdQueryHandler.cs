using Common.Exceptions;
using ExpenseService.Api.Interfaces;
using ExpenseService.Api.Models;
using ExpenseService.Api.Queries;
using MediatR;

namespace ExpenseService.Api.Handlers
{
    public class DeleteExpenseByIdQueryHandler : IRequestHandler<DeleteExpenseByIdQuery, ExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        public DeleteExpenseByIdQueryHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        public async Task<ExpenseResponse> Handle(DeleteExpenseByIdQuery request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.DeleteExpense(request.Id) ?? throw new ExpenseNotFoundException("Expense with this Id does not exists");
            return expense;
        }
    }
}
