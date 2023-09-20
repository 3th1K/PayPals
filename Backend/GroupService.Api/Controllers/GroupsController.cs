using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using FluentValidation;
using GroupService.Api.Models;
using GroupService.Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using System.Security.Claims;

namespace GroupService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<GroupsController> _logger;
        private IErrorBuilder _errorBuilder;
        private IExceptionHandler _exceptionHandler;
        public GroupsController(
            IMediator mediator, 
            ILogger<GroupsController> logger, 
            IErrorBuilder errorBuilder,
            IExceptionHandler exceptionHandler)
        {
            _mediator = mediator;
            _logger = logger;
            _errorBuilder = errorBuilder;
            _exceptionHandler = exceptionHandler;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] GroupRequest request)
        {
            var authenticatedUserId = User.FindFirst("userId")?.Value;
            var authenticatedUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (authenticatedUserRole != "Admin" && authenticatedUserId != request.CreatorId.ToString())
            {
                var error = _errorBuilder.BuildError(Errors.ERR_USER_FORBIDDEN);
                return new ObjectResult(error) { StatusCode = 403 };
            }
            return await _exceptionHandler.HandleException<Exception>(async () =>
            {
                var data = await _mediator.Send(request);
                return Ok(data);
            });

        }


        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            
            try
            {
                var data = await _mediator.Send(new GetGroupByIdQuery(id));
                return Ok(data);
            }
            catch (ValidationException ex)
            {
                var validationErrors = ex.Errors.Select(error => error.ErrorMessage).ToList();
                var error = _errorBuilder.BuildError(ex, ex.Message, validationErrors);
                return BadRequest(error);
            }
            catch (UserNotAuthorizedException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return Unauthorized(error);
            }
            catch (GroupNotFoundException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return NotFound(error);
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
                var error = _errorBuilder.BuildError(ex, ex.Message, validationErrors);
                return BadRequest(error);
            }
            catch (UserForbiddenException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 403 };
            }
            catch (GroupNotFoundException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return NotFound(error);
            }

        }


    }
}
