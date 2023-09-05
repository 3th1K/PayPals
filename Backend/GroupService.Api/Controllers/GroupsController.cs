using FluentValidation;
using GroupService.Api.Exceptions;
using GroupService.Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroupService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GroupsController> _logger;
        public GroupsController(IMediator mediator, ILogger<GroupsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
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
                var data = await _mediator.Send(new GetGroupByIdQuery(id));
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
                    ErrorMessage = ex.ErrorMessage
                });
            }
            catch (GroupNotFoundException ex)
            {
                return NotFound(new ErrorResponse
                {
                    ErrorCode = ex.ErrorCode,
                    ErrorMessage = ex.ErrorMessage
                });
            }

        }

        [HttpGet]
        [Route("{id}/expenses")]
        [Authorize]
        public async Task<IActionResult> GetExpensesById(int id)
        {
            try
            {
                var data = await _mediator.Send(new GetGroupExpensesByIdQuery(id));
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
                    ErrorMessage = ex.ErrorMessage
                });
            }
            catch (GroupNotFoundException ex)
            {
                return NotFound(new ErrorResponse
                {
                    ErrorCode = ex.ErrorCode,
                    ErrorMessage = ex.ErrorMessage
                });
            }

        }

        public class ErrorResponse
        {
            public string ErrorCode { get; set; } = null!;
            public string ErrorMessage { get; set; } = null!;
        }


    }
}
