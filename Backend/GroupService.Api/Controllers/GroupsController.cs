using Common.Exceptions;
using Common.Interfaces;
using FluentValidation;
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
        private IErrorBuilder _errorBuilder;
        public GroupsController(
            IMediator mediator, 
            ILogger<GroupsController> logger, 
            IErrorBuilder errorBuilder)
        {
            _mediator = mediator;
            _logger = logger;
            _errorBuilder = errorBuilder;
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


    }
}
