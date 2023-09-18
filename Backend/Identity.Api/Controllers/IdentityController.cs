using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using FluentValidation;
using Identity.Api.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Identity.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<IdentityController> _logger;
        private IErrorBuilder _errorBuilder;
        public IdentityController(IMediator mediator, ILogger<IdentityController> logger, IErrorBuilder errorBuilder)
        {
            _mediator = mediator;
            _logger = logger;  
            _errorBuilder = errorBuilder;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestCommand request)
        {
            try
            {
                var token = await _mediator.Send(request);
                return Ok(token);
            }
            catch (ValidationException ex)
            {
                var validationErrors = ex.Errors.Select(error => error.ErrorMessage).ToList();
                var error = _errorBuilder.BuildError(ex, ex.Message, validationErrors);
                return BadRequest(error);
            }
            catch (UserNotFoundException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return NotFound(error);
            }
            catch (UserNotAuthorizedException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return Unauthorized(error);
            }
            catch (Exception ex) {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 500 };
            }
        }
        [HttpGet]
        [Route("healthcheck")]
        public IActionResult HealthCheck() {
            _logger.LogInformation("someone tried to ping");
            return Ok();
        }

    }
}
