using ExpenseService.Api.Exceptions;
using ExpenseService.Api.Models;
using ExpenseService.Api.Queries;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;

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
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ExpenseRequest expenseRequest)
        {
            var headers = Request.Headers;
            var headersDictionary = new Dictionary<string, string>();

            // Iterate through the headers and add them to the dictionary
            foreach (var header in headers)
            {
                // header.Key contains the header name
                // header.Value is an IEnumerable<string> containing the header values
                string headerName = header.Key;
                string headerValue = string.Join(",", header.Value);

                // Add the header to the dictionary
                headersDictionary.Add(headerName, headerValue);
            }
            try
            {
                var data = await _mediator.Send(expenseRequest);
                return Ok(data);
            }
            catch (ValidationException ex)
            {
                var validationErrors = ex.Errors.Select(error => error.ErrorMessage).ToList();
                return BadRequest(new { Errors = validationErrors });
            }
            catch (UserNotAuthorizedException ex)
            {
                return Unauthorized(new ErrorResponse
                {
                    ErrorCode = ex.ErrorCode,
                    ErrorMessage = ex.ErrorMessage,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
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
                return BadRequest(new { Errors = validationErrors });
            }
            catch (ExpenseNotFoundException ex)
            {
                return Unauthorized(new ErrorResponse
                {
                    ErrorCode = ex.ErrorCode,
                    ErrorMessage = ex.ErrorMessage,
                });
            }
            catch (UserNotAuthorizedException ex)
            {
                return Unauthorized(new ErrorResponse
                {
                    ErrorCode = ex.ErrorCode,
                    ErrorMessage = ex.ErrorMessage,
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        public class ErrorResponse
        {
            public string ErrorCode { get; set; } = null!;
            public string ErrorMessage { get; set; } = null!;
        }
    }
}
