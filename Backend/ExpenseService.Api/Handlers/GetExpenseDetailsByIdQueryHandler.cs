﻿using System.Security.Claims;
using Common.Exceptions;
using Data.DTOs.ExpenseDTOs;
using ExpenseService.Api.Interfaces;
using ExpenseService.Api.Queries;
using MediatR;

namespace ExpenseService.Api.Handlers
{
    public class GetExpenseDetailsByIdQueryHandler : IRequestHandler<GetExpenseDetailsByIdQuery, ExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetExpenseDetailsByIdQueryHandler(IExpenseRepository expenseRepository, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _expenseRepository = expenseRepository;   
        }
        public async Task<ExpenseResponse> Handle(GetExpenseDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var expense = await _expenseRepository.GetExpenseDetails(request.Id)?? throw new ExpenseNotFoundException("Expense Was Not Found");
            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            if (
                authenticatedUserRole != "Admin" &&
                authenticatedUserId  != expense.PayerId.ToString() &&
                expense.Users.SingleOrDefault(u => u.UserId.ToString().Equals(authenticatedUserId)) == null
            )
            {
                throw new UserForbiddenException("User is not Authorized To Access This Content");
            }
            return expense;
        }
    }
}
