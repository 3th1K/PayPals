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
        private IExceptionHandler _exceptionHandler;


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
            var userId = User.FindFirst("userId")?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            return (userId, userRole);
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


    }
}
