using System.Security.Claims;
using Common.Exceptions;
using Common.Interfaces;
using Data.DTOs.GroupDTOs;
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
        private readonly IExceptionHandler _exceptionHandler;


        public GroupsController(
            IMediator mediator, 
            ILogger<GroupsController> logger, 
            IExceptionHandler exceptionHandler)
        {
            _mediator = mediator;
            _logger = logger;
            _exceptionHandler = exceptionHandler;
        }

        private (string? UserId, string? UserRole) GetAuthenticatedUser()
        {
            _logger.LogDebug("Getting Authenticated User");
            var userId = User.FindFirst("userId")?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            return (userId, userRole);
        }

        [HttpGet]
        [Route("allgroups")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() {
            return await _exceptionHandler.HandleException<Exception>(async () =>
            {
                var data = await _mediator.Send(new GetAllGroupsQuery());
                return Ok(data);
            });
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] GroupRequest request)
        {
            return await _exceptionHandler.HandleException<Exception>(async () =>
            {
                var (authenticatedUserId, authenticatedUserRole) = GetAuthenticatedUser();
                if (authenticatedUserRole != "Admin" && authenticatedUserId != request.CreatorId.ToString()) 
                {
                    throw new UserForbiddenException("User is not allowed to create this group");
                }
                var data = await _mediator.Send(request);
                return Ok(data);
            });

        }

        [HttpPut]
        [Route("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] GroupUpdateRequest request) {
            return await _exceptionHandler.HandleException<Exception>(async () => {
                var data = await _mediator.Send(request);
                return Ok(data);
            });
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            
            return await _exceptionHandler.HandleException<Exception>(async () =>
            {
                var data = await _mediator.Send(new GetGroupByIdQuery(id));
                return Ok(data);
            });
        }

        [HttpGet]
        [Route("{id}/expenses")]
        [Authorize]
        public async Task<IActionResult> GetExpensesById(int id)
        {
            return await _exceptionHandler.HandleException<Exception>(async () =>
            {
                var data = await _mediator.Send(new GetGroupExpensesByIdQuery(id));
                return Ok(data);
            });
            
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return await _exceptionHandler.HandleException<Exception>(async () => {
                var data = await _mediator.Send(new DeleteGroupQuery(id));
                return Ok(data);
            });
        }

        [HttpPut]
        [Route("{id}/member")]
        [Authorize]
        public async Task<IActionResult> AddMember(int id, [FromBody] GroupMember groupMember) {
            return await _exceptionHandler.HandleException<Exception>(async ()=> {
                var data = await _mediator.Send(new AddMemberQuery(id, groupMember.UserId));
                return Ok(data);
            });
        }

        [HttpDelete]
        [Route("{id}/member")]
        [Authorize]
        public async Task<IActionResult> DeleteMember(int id, [FromBody] GroupMember groupMember)
        {
            return await _exceptionHandler.HandleException<Exception>(async () => {
                var data = await _mediator.Send(new DeleteMemberQuery(id, groupMember.UserId));
                return Ok(data);
            });
        }
    }
}
