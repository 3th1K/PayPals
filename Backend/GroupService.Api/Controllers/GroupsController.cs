﻿using FluentValidation;
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
            catch (ValidationException ex)
            {
                var validationErrors = ex.Errors.Select(error => error.ErrorMessage).ToList();
                return BadRequest(new { Errors = validationErrors });
            }
            catch (UserNotAuthorizedException ex)
            {
                return Unauthorized(new ErrorResponse
                {
                    ErrorCode = ex.ErrorCode,
                    ErrorMessage = ex.ErrorMessage
                });
            }
            catch (GroupNotFoundException ex)
            {
                return NotFound(new ErrorResponse
                {
                    ErrorCode = ex.ErrorCode,
                    ErrorMessage = ex.ErrorMessage
                });
            }

        }

        [HttpGet]
        [Route("{id}/expenses")]
        [Authorize]
        public async Task<IActionResult> GetExpensesById(int id)
        {
            try
            {
                var data = await _mediator.Send(new GetGroupExpensesByIdQuery(id));
                return Ok(data);
            }
            catch (ValidationException ex)
            {
                var validationErrors = ex.Errors.Select(error => error.ErrorMessage).ToList();
                return BadRequest(new { Errors = validationErrors });
            }
            catch (UserNotAuthorizedException ex)
            {
                return Unauthorized(new ErrorResponse
                {
                    ErrorCode = ex.ErrorCode,
                    ErrorMessage = ex.ErrorMessage
                });
            }
            catch (GroupNotFoundException ex)
            {
                return NotFound(new ErrorResponse
                {
                    ErrorCode = ex.ErrorCode,
                    ErrorMessage = ex.ErrorMessage
                });
            }

        }

        public class ErrorResponse
        {
            public string ErrorCode { get; set; } = null!;
            public string ErrorMessage { get; set; } = null!;
        }


    }
}
