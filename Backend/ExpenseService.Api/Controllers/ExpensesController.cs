using ExpenseService.Api.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ILogger<ExpensesController> _logger;
        private readonly IMediator _mediator;
        public ExpensesController(ILogger<ExpensesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ExpenseRequest expenseRequest) 
        {
            try
            {
                var data = await _mediator.Send(expenseRequest);
                return Ok(data);
            }
            catch (Exception ex) 
            {
                return BadRequest(ex);
            }
        
        }
    }
}
