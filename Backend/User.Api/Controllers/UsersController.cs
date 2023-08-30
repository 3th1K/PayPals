using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserService.Api.Models;
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
        public UsersController(IMediator mediator, ILogger<UsersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet]
        [Route("allusers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users =  await _mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }
        [HttpGet]
        [Route("allusers/details")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllDetails()
        {
            var users = await _mediator.Send(new GetAllUsersDetailsQuery());
            return Ok(users);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var authenticatedUserId = User.FindFirst("userId")?.Value;
            var authenticatedUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (authenticatedUserRole!="Admin" && authenticatedUserId != id.ToString())
            {
                return Forbid(); // Return 403 Forbidden if unauthorized
            }
            var data = await _mediator.Send(new GetUserByIdQuery(id));
            return data.Match<IActionResult>(
                result => result == null ? NotFound() : Ok(result),
                error => BadRequest(error)
            );
        }

        
        [HttpGet]
        [Route("{id}/groups")]
        public async Task<IActionResult> GetUserGroups(int id)
        {
            var authenticatedUserId = User.FindFirst("userId")?.Value;
            var authenticatedUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (authenticatedUserRole != "Admin" && authenticatedUserId != id.ToString())
            {
                return Forbid(); // Return 403 Forbidden if unauthorized
            }
            try {
                var data = await _mediator.Send(new GetUserGroupsQuery(id));
                return Ok(data);
            }
            catch (UserNotFoundException ex) { 
                return BadRequest(new ErrorResponse
                {
                    ErrorCode = ex.ErrorCode,
                    ErrorMessage = ex.ErrorMessage
                });
            }
            
        }

        [HttpGet]
        [Route("details/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDetailsById(int id)
        {
            var data = await _mediator.Send(new GetUserDetailsByIdQuery(id));
            return data.Match<IActionResult>(
                result => result == null ? NotFound() : Ok(result),
                error => BadRequest(error)
            );

        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            _logger.LogInformation("hello");
            try
            {
                var data = await _mediator.Send(request);
                return data.Match<IActionResult>(
                    result => Ok(result),
                    error => BadRequest(error)
                );
            }
            catch (UserAlreadyExistsException ex) {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var data = await _mediator.Send(new DeleteUserQuery(id));
            return data.Match<IActionResult>(
                result => result == null ? NotFound() : Ok(result),
                error => BadRequest(error)
            );
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest request) 
        {
            var authenticatedUserId = User.FindFirst("userId")?.Value;
            var authenticatedUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (authenticatedUserRole != "Admin" && authenticatedUserId != request.UserId.ToString())
            {
                return Forbid(); // Return 403 Forbidden if unauthorized
            }
            var data = await _mediator.Send(request);
            return data.Match<IActionResult>(
                result => result == null ? NotFound() : Ok(result),
                error => BadRequest(error)
            );
        }

        public class ErrorResponse
        {
            public string ErrorCode { get; set; }
            public string ErrorMessage { get; set; }
        }
    }
}
