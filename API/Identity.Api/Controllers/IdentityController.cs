using Common.Interfaces;
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
        private readonly IExceptionHandler _exceptionHandler;
        public IdentityController(
            IMediator mediator, 
            ILogger<IdentityController> logger, 
            IExceptionHandler exceptionHandler
            )
        {
            _mediator = mediator;
            _logger = logger;  
            _exceptionHandler = exceptionHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestCommand request)
        {
            //return await _exceptionHandler.HandleException<Exception>(async () => {
            //    var token = await _mediator.Send(request);
            //    return Ok(token);
            //});
            var token = await _mediator.Send(request);
            return token.Result;
        }
        [HttpGet]
        [Route("healthcheck")]
        public IActionResult HealthCheck() {
            _logger.LogInformation("someone tried to ping");
            return Ok();
        }

    }
}
