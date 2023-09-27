﻿using Common.DTOs.ExpenseDTOs;
using Common.Exceptions;
using Common.Interfaces;
using MediatR;

namespace ExpenseService.Api.Handlers
{
    public class ExpenseApprovalRequestHandler : IRequestHandler<ExpenseApprovalRequest, ExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IUserRepository _userRepository;
        public ExpenseApprovalRequestHandler(IExpenseRepository expenseRepository, IUserRepository userRepository)
        {
            _expenseRepository = expenseRepository;
            _userRepository = userRepository;
        }
        public async Task<ExpenseResponse> Handle(ExpenseApprovalRequest request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetExpenseDetails(request.ExpenseId) 
                          ?? throw new ExpenseNotFoundException($"Expense with {request.ExpenseId} is not present");
            
            _ = await _userRepository.GetUserById(request.UserId) 
                ?? throw new UserNotFoundException("User in the approve request is not a valid user");

            var existingApproval = expense.ExpenseApprovals.FirstOrDefault(ea => ea.UserId == request.UserId);
            if (existingApproval != null) throw new ApprovalAlreadyExistsException("User already given the approval for this expense");
            var expenseResponse = await _expenseRepository.SubmitExpenseApproval(request);
            return expenseResponse;
        }
    }
}