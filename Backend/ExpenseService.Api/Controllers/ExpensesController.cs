using System.Security.Claims;
using Common.Exceptions;
using Common.Interfaces;
using ExpenseService.Api.Models;
using ExpenseService.Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ILogger<ExpensesController> _logger;
        private readonly IMediator _mediator;
        private readonly  IExceptionHandler _exceptionHandler;
        public ExpensesController(ILogger<ExpensesController> logger,
            IMediator mediator,
            IExceptionHandler exceptionHandler)
        {
            _logger = logger;
            _mediator = mediator;
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
        [Route("allexpenses")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            return await _exceptionHandler.HandleException<Exception>(async () =>
            {
                var data = await _mediator.Send(new GetAllExpensesQuery());
                return Ok(data);
            });
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] ExpenseRequest expenseRequest)
        {
            _logger.LogInformation("Creating new Expense");
            return await _exceptionHandler.HandleException<Exception>(async () => {
                var data = await _mediator.Send(expenseRequest);
                return Ok(data);
            });
        }

        [HttpGet]
        [Route("{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetDetails(int id) 
        {
            return await _exceptionHandler.HandleException<Exception>(async () => {
                var data = await _mediator.Send(new GetExpenseDetailsByIdQuery(id));
                return Ok(data);
            });
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            return await _exceptionHandler.HandleException<Exception>(async () => {
                var (authenticatedUserId, authenticatedUserRole) = GetAuthenticatedUser();
                if (authenticatedUserRole != "Admin" && authenticatedUserId != id.ToString())
                {
                    throw new UserForbiddenException("User is not allowed to access this content");
                }
                var data = await _mediator.Send(new DeleteExpenseByIdQuery(id));
                return Ok(data);
            });
        }

    }
}
