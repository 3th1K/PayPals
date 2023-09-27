using Common.Exceptions;
using Data.DTOs.ExpenseDTOs;
using ExpenseService.Api.Interfaces;
using MediatR;

namespace ExpenseService.Api.Handlers
{
    public class ExpenseApprovalRequestHandler : IRequestHandler<ExpenseApprovalRequest, ExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        public ExpenseApprovalRequestHandler(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }
        public async Task<ExpenseResponse> Handle(ExpenseApprovalRequest request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetExpenseDetails(request.ExpenseId) 
                          ?? throw new ExpenseNotFoundException($"Expense with {request.ExpenseId} is not present");

            var existingApproval = expense.ExpenseApprovals.FirstOrDefault(ea => ea.UserId == request.UserId);
            if (existingApproval != null) throw new ApprovalAlreadyExistsException("User already given the approval for this expense");
            var expenseResponse = await _expenseRepository.SubmitExpenseApproval(request);
            return expenseResponse;
        }
    }
}
