using System.Security.Claims;
using Common.DTOs.UserDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
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
            var data = await _mediator.Send(new GetAllUsersQuery());
            return data.Result;
        }

        [HttpGet]
        [Route("allusers/details")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDetails()
        {
            try
            {
                var users = await _mediator.Send(new GetAllUsersDetailsQuery());
                return Ok(users);
            }
            catch (ApiException ex)
            {
                return ex.Result;
            }

            

        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var (authenticatedUserId, authenticatedUserRole) = GetAuthenticatedUser();
            if (authenticatedUserRole != "Admin" && authenticatedUserId != id.ToString())
            {
                return ApiResult<Error>
                    .Failure(ErrorType.ErrUserForbidden, "User is not allowed to access this content").Result;
            }
            var data = await _mediator.Send(new GetUserByIdQuery(id));
            return data.Result;
        }


        [HttpGet]
        [Route("{id:int}/groups")]
        [Authorize]
        public async Task<IActionResult> GetUserGroups(int id)
        {
            var (authenticatedUserId, authenticatedUserRole) = GetAuthenticatedUser();
            if (authenticatedUserRole != "Admin" && authenticatedUserId != id.ToString())
            {
                return ApiResult<Error>
                    .Failure(ErrorType.ErrUserForbidden, "User is not allowed to access this content").Result;
            }
            var data = await _mediator.Send(new GetUserGroupsQuery(id));
            return data.Result;
        }

        [HttpGet]
        [Route("{id}/expenses")]
        [Authorize]
        public async Task<IActionResult> GetUserExpenses(int id)
        {
            var (authenticatedUserId, authenticatedUserRole) = GetAuthenticatedUser();
            if (authenticatedUserRole != "Admin" && authenticatedUserId != id.ToString())
            {
                return ApiResult<Error>.Failure(ErrorType.ErrUserForbidden, "User do not have access to this content").Result;
            }
            var data = await _mediator.Send(new GetUserExpensesQuery(id));
            return data.Result;

        }

        [HttpGet]
        [Route("details/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDetailsById(int id)
        {
            var data = await _mediator.Send(new GetUserDetailsByIdQuery(id));
            return data.Result;
        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            var data = await _mediator.Send(request);
            return data.Result;
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _mediator.Send(new DeleteUserQuery(id));
            return data.Result;
        }

        [HttpPut]
        [Route("update")]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest request)
        {
            var (authenticatedUserId, authenticatedUserRole) = GetAuthenticatedUser();
            if (authenticatedUserRole != "Admin" && authenticatedUserId != request.UserId.ToString())
            {
                return ApiResult<Error>
                    .Failure(ErrorType.ErrUserForbidden, "User is not allowed to access this content").Result;
            }
            var data = await _mediator.Send(request);
            return data.Result;
        }
    }
}
