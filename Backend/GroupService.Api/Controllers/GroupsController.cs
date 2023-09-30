using System.Security.Claims;
using Common.DTOs.GroupDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
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
            var data = await _mediator.Send(new GetAllGroupsQuery());
            return data.Result;
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] GroupRequest request)
        {
            var (authenticatedUserId, authenticatedUserRole) = GetAuthenticatedUser();
            if (authenticatedUserRole != "Admin" && authenticatedUserId != request.CreatorId.ToString())
            {
                return ApiResult<Error>.Failure(ErrorType.ErrUserForbidden, "User is not allowed to create this group").Result;
            }
            var data = await _mediator.Send(request);
            return data.Result;

        }

        [HttpPut]
        [Route("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] GroupUpdateRequest request) {
            var data = await _mediator.Send(request);
            return data.Result;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _mediator.Send(new GetGroupByIdQuery(id));
            return data.Result;
        }

        [HttpGet]
        [Route("{id}/expenses")]
        [Authorize]
        public async Task<IActionResult> GetExpensesById(int id)
        {
            var data = await _mediator.Send(new GetGroupExpensesByIdQuery(id));
            return data.Result;

        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _mediator.Send(new DeleteGroupQuery(id));
            return data.Result;
        }

        [HttpPut]
        [Route("{id}/member")]
        [Authorize]
        public async Task<IActionResult> AddMember(int id, [FromBody] GroupMember groupMember) {
            var data = await _mediator.Send(new AddMemberQuery(id, groupMember.UserId));
            return data.Result;
        }

        [HttpDelete]
        [Route("{id}/member")]
        [Authorize]
        public async Task<IActionResult> DeleteMember(int id, [FromBody] GroupMember groupMember)
        {
            var data = await _mediator.Send(new DeleteMemberQuery(id, groupMember.UserId));
            return data.Result;
        }
    }
}
