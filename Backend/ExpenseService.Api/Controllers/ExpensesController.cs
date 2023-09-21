using Common.Exceptions;
using Common.Interfaces;
using ExpenseService.Api.Models;
using ExpenseService.Api.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ILogger<ExpensesController> _logger;
        private readonly IMediator _mediator;
        private IExceptionHandler _exceptionHandler;
        public ExpensesController(ILogger<ExpensesController> logger, IMediator mediator, IExceptionHandler exceptionHandler)
        {
            _logger = logger;
            _mediator = mediator;
            _exceptionHandler = exceptionHandler;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ExpenseRequest expenseRequest)
        {
            return await _exceptionHandler.HandleException<Exception>(async () => {
                var data = await _mediator.Send(expenseRequest);
                return Ok(data);
            });
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetDetails(int id) 
        {
            return await _exceptionHandler.HandleException<Exception>(async () => {
                var data = await _mediator.Send(new GetExpenseDetailsByIdQuery(id));
                return Ok(data);
            });
        }
    }
}
