﻿using AutoMapper;
using ExpenseService.Api.Exceptions;
using ExpenseService.Api.Interfaces;
using ExpenseService.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Security.Claims;

namespace ExpenseService.Api.Handlers
{
    public class ExpenseRequestHandler : IRequestHandler<ExpenseRequest, ExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ExpenseRequestHandler> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ExpenseRequestHandler(IExpenseRepository expenseRepository, IMapper mapper, ILogger<ExpenseRequestHandler> logger, IHttpContextAccessor httpContextAccessor)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ExpenseResponse> Handle(ExpenseRequest request, CancellationToken cancellationToken)
        {
            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            if (
                authenticatedUserRole != "Admin" &&
                request.PayerId.ToString() != authenticatedUserId
            )
            {
                throw new UserNotAuthorizedException();
            }
            var expenseResponse = await _expenseRepository.CreateExpense(request);
            return expenseResponse;


        }
    }
}