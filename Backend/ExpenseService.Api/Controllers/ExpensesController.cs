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
        private IErrorBuilder _errorBuilder;
        public ExpensesController(ILogger<ExpensesController> logger, IMediator mediator, IErrorBuilder errorBuilder)
        {
            _logger = logger;
            _mediator = mediator;
            _errorBuilder = errorBuilder;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ExpenseRequest expenseRequest)
        {
            try
            {
                var data = await _mediator.Send(expenseRequest);
                return Ok(data);
            }
            catch (ValidationException ex)
            {
                var validationErrors = ex.Errors.Select(error => error.ErrorMessage).ToList();
                var error = _errorBuilder.BuildError(ex, ex.Message, validationErrors);
                return BadRequest(error);
            }
            catch (UserForbiddenException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 403 };
            }
            catch (Exception ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 500 };
            }

        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetDetails(int id) 
        {
            try
            {
                var data = await _mediator.Send(new GetExpenseDetailsByIdQuery(id));
                return Ok(data);
            }
            catch (ValidationException ex)
            {
                var validationErrors = ex.Errors.Select(error => error.ErrorMessage).ToList();
                var error = _errorBuilder.BuildError(ex, ex.Message, validationErrors);
                return BadRequest(error);
            }
            catch (ExpenseNotFoundException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return NotFound(error);
            }
            catch (UserForbiddenException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 403 };
            }
            catch (Exception ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 500 };
            }

        }
    }
}
