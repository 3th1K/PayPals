using Identity.Api.Commands;
using Identity.Api.Exceptions;
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
        public IdentityController(IMediator mediator, ILogger<IdentityController> logger)
        {
            _mediator = mediator;
            _logger = logger;  

        }

        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestCommand request)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("request data is not valid");
                return BadRequest(ModelState);
            }
            try 
            {
                var token = await _mediator.Send(request);
                return Ok(token);
            }
            catch (UserNotFoundException)
            {
                // do something
                return NotFound();
            }
            catch (UserNotAuthorizedException)
            {
                // do something
                return Unauthorized();
            }
        }
        [HttpGet]
        [Route("/healthcheck")]
        public IActionResult HealthCheck() {
            _logger.LogInformation("someone tried to ping");
            return Ok();
        }

    }
}
