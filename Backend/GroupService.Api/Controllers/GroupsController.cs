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
            try
            {
                var data = await _mediator.Send(new GetGroupByIdQuery(id));
                return Ok(data);
            }
            catch (UserNotAuthorizedException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    ErrorCode = ex.ErrorCode,
                    ErrorMessage = ex.ErrorMessage
                });
            }
            catch (GroupNotFoundException ex)
            {
                return BadRequest(new ErrorResponse
                {
                    ErrorCode = ex.ErrorCode,
                    ErrorMessage = ex.ErrorMessage
                });
            }

        }

        public class ErrorResponse
        {
            public string ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
        }


    }
}
