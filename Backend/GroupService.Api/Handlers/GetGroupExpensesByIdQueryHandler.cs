using Common.Exceptions;
using GroupService.Api.Interfaces;
using GroupService.Api.Models;
using GroupService.Api.Queries;
using MediatR;
using System.Security.Claims;

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
                throw new UserNotAuthorizedException();
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
            catch (Exception)
            {
                throw;
            }
        }
    }
}
