﻿using MediatR;
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
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
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
        public async Task<IActionResult> Create([FromBody] UserRequest request)
        {
            var data = await _mediator.Send(request);
            return data.Match<IActionResult>(
                result => Ok(result) ,
                error => BadRequest(error)
            );
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
    }
}