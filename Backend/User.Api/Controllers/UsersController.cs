using System.Security.Claims;
using Common.DTOs.UserDTOs;
using Common.Exceptions;
using Common.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Api.Queries;

namespace UserService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UsersController> _logger;
        private readonly IExceptionHandler _exceptionHandler;
        
        public UsersController(
            IMediator mediator, 
            ILogger<UsersController> logger,
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
        [Route("allusers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            return await _exceptionHandler.HandleException<Exception>(async () =>
            {
                var users = await _mediator.Send(new GetAllUsersQuery());
                return Ok(users);
            });
            
        }

        [HttpGet]
        [Route("allusers/details")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDetails()
        {
            return await _exceptionHandler.HandleException<Exception>(async () =>
            {
                var users = await _mediator.Send(new GetAllUsersDetailsQuery());
                return Ok(users);
            });
            
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            return await _exceptionHandler.HandleException<Exception>(async () =>
            {
                var (authenticatedUserId, authenticatedUserRole) = GetAuthenticatedUser();
                if (authenticatedUserRole != "Admin" && authenticatedUserId != id.ToString())
                {
                    throw new UserForbiddenException("User is not allowed to access this content");
                }
                var data = await _mediator.Send(new GetUserByIdQuery(id));
                return Ok(data);
            });
        }

        
        [HttpGet]
        [Route("{id:int}/groups")]
        [Authorize]
        public async Task<IActionResult> GetUserGroups(int id)
        {
            return await _exceptionHandler.HandleException<Exception>(async () =>
            {
                var (authenticatedUserId, authenticatedUserRole) = GetAuthenticatedUser();
                if (authenticatedUserRole != "Admin" && authenticatedUserId != id.ToString())
                {
                    throw new UserForbiddenException("User is not allowed to access this content");
                }
                var data = await _mediator.Send(new GetUserGroupsQuery(id));
                return Ok(data);
            });
        }

        [HttpGet]
        [Route("{id}/expenses")]
        [Authorize]
        public async Task<IActionResult> GetUserExpenses(int id)
        {
            return await _exceptionHandler.HandleException<Exception>(async () =>
            {
                var (authenticatedUserId, authenticatedUserRole) = GetAuthenticatedUser();
                if (authenticatedUserRole != "Admin" && authenticatedUserId != id.ToString())
                {
                    throw new UserForbiddenException("User is not allowed to access this content");
                }
                var data = await _mediator.Send(new GetUserExpensesQuery(id));
                return Ok(data);
            });
        }

        [HttpGet]
        [Route("details/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDetailsById(int id)
        {
            return await _exceptionHandler.HandleException<Exception>(async () =>
            {
                var data = await _mediator.Send(new GetUserDetailsByIdQuery(id));
                return Ok(data);
            });
        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            return await _exceptionHandler.HandleException<Exception>(async () => {
                var data = await _mediator.Send(request);
                return Ok(data);
            });

        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            return await _exceptionHandler.HandleException<Exception>(async () => {
                var data = await _mediator.Send(new DeleteUserQuery(id));
                return Ok(data);
            });
        }

        [HttpPut]
        [Route("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest request) 
        {
            return await _exceptionHandler.HandleException<Exception>(async () => {
                var (authenticatedUserId, authenticatedUserRole) = GetAuthenticatedUser();
                if (authenticatedUserRole != "Admin" && authenticatedUserId != request.UserId.ToString())
                {
                    throw new UserForbiddenException("User is not allowed to access this content");
                }
                var data = await _mediator.Send(request);
                return Ok(data);
            });
        }
    }
}
