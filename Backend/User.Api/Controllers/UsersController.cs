using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using FluentValidation;
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
        private IErrorBuilder _errorBuilder;
        public UsersController(IMediator mediator, ILogger<UsersController> logger, IErrorBuilder errorBuilder)
        {
            _mediator = mediator;
            _logger = logger;
            _errorBuilder = errorBuilder;
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
        [Authorize]
        public async Task<IActionResult> GetById(int id)
        {
            var authenticatedUserId = User.FindFirst("userId")?.Value;
            var authenticatedUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (authenticatedUserRole!="Admin" && authenticatedUserId != id.ToString())
            {
                var error = _errorBuilder.BuildError(Errors.ERR_USER_FORBIDDEN);
                return new ObjectResult(error) { StatusCode = 403 };
            }
            
            try 
            {
                var data = await _mediator.Send(new GetUserByIdQuery(id));
                return Ok(data);
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
            catch (Exception ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 500 };
            }
        }

        
        [HttpGet]
        [Route("{id}/groups")]
        public async Task<IActionResult> GetUserGroups(int id)
        {
            var authenticatedUserId = User.FindFirst("userId")?.Value;
            var authenticatedUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (authenticatedUserRole != "Admin" && authenticatedUserId != id.ToString())
            {
                var error = _errorBuilder.BuildError(Errors.ERR_USER_FORBIDDEN);
                return new ObjectResult(error) { StatusCode = 403 };
            }
            try {
                var data = await _mediator.Send(new GetUserGroupsQuery(id));
                return Ok(data);
            }
            catch (ValidationException ex)
            {
                var validationErrors = ex.Errors.Select(error => error.ErrorMessage).ToList();
                var error = _errorBuilder.BuildError(ex, ex.Message, validationErrors);
                return BadRequest(error);
            }
            catch (UserNotFoundException ex) { 
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return NotFound(error);
            }
            catch (Exception ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 500 };
            }

        }

        [HttpGet]
        [Route("details/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetDetailsById(int id)
        {
            try
            {
                var data = await _mediator.Send(new GetUserDetailsByIdQuery(id));
                return Ok(data);
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
            catch (Exception ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 500 };
            }

        }

        [HttpPost]
        [Route("create")]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            try 
            {
                var data = await _mediator.Send(request);
                return Ok(data);
            }
            catch (ValidationException ex)
            {
                var validationErrors = ex.Errors.Select(error => error.ErrorMessage).ToList();
                var error = _errorBuilder.BuildError(ex, ex.Message, validationErrors);
                return BadRequest(error);
            }
            catch (UserAlreadyExistsException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return Conflict(error);
            }
            catch (Exception ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 500 };
            }

        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var data = await _mediator.Send(new DeleteUserQuery(id));
                return Ok(data);
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
            catch (Exception ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 500 };
            }
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] UserUpdateRequest request) 
        {
            var authenticatedUserId = User.FindFirst("userId")?.Value;
            var authenticatedUserRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (authenticatedUserRole != "Admin" && authenticatedUserId != request.UserId.ToString())
            {
                var error = _errorBuilder.BuildError(Errors.ERR_USER_FORBIDDEN);
                return new ObjectResult(error) { StatusCode = 403 };
            }
            
            try
            {
                var data = await _mediator.Send(request);
                return Ok(data);
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
            catch (Exception ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 500 };
            }
        }
    }
}
