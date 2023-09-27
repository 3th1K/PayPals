using System.Security.Claims;
using Common.Exceptions;
using Data.DTOs.ExpenseDTOs;
using GroupService.Api.Interfaces;
using GroupService.Api.Queries;
using MediatR;

namespace GroupService.Api.Handlers
{
    public class GetGroupExpensesByIdQueryHandler : IRequestHandler<GetGroupExpensesByIdQuery, List<ExpenseResponse>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetGroupExpensesByIdQueryHandler(IGroupRepository groupRepository, IHttpContextAccessor httpContextAccessor)
        {
            _groupRepository = groupRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<ExpenseResponse>> Handle(GetGroupExpensesByIdQuery request, CancellationToken cancellationToken)
        {
            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            if (
                authenticatedUserRole != "Admin" &&
                !await _groupRepository.CheckUserExistenceInGroup(request.Id, int.Parse(authenticatedUserId!))
            )
            {
                throw new UserForbiddenException("User is not allowed to access this content");
            }

            try
            {
                var expenses = await _groupRepository.GetGroupExpensesById(request.Id);
                return expenses;
            }
            catch (GroupNotFoundException ex) 
            {
                throw;
            }
        }
    }
}
