using AutoMapper;
using ExpenseService.Api.Interfaces;
using ExpenseService.Api.Models;
using MediatR;

namespace ExpenseService.Api.Handlers
{
    public class ExpenseRequestHandler : IRequestHandler<ExpenseRequest, ExpenseResponse>
    {
        private readonly IExpenseRepository _expenseRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ExpenseRequestHandler> _logger;
        public ExpenseRequestHandler(IExpenseRepository expenseRepository, IMapper mapper, ILogger<ExpenseRequestHandler> logger)
        {
            _expenseRepository = expenseRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<ExpenseResponse> Handle(ExpenseRequest request, CancellationToken cancellationToken)
        {
            //var expense = _mapper.Map<Expense>(request);
            var expenseResponse = await _expenseRepository.CreateExpense(request);
            return expenseResponse;
        }
    }
}
