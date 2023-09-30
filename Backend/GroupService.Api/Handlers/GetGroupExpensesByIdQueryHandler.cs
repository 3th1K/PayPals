using System.Security.Claims;
using Common.DTOs.ExpenseDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using GroupService.Api.Queries;
using MediatR;

namespace GroupService.Api.Handlers
{
    public class GetGroupExpensesByIdQueryHandler : IRequestHandler<GetGroupExpensesByIdQuery, ApiResult<List<ExpenseResponse>>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetGroupExpensesByIdQueryHandler(IGroupRepository groupRepository, IHttpContextAccessor httpContextAccessor)
        {
            _groupRepository = groupRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<List<ExpenseResponse>>> Handle(GetGroupExpensesByIdQuery request, CancellationToken cancellationToken)
        {
            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            if (
                authenticatedUserRole != "Admin" &&
                !await _groupRepository.CheckUserExistenceInGroup(request.Id, int.Parse(authenticatedUserId!))
            )
            {
                return ApiResult<List<ExpenseResponse>>.Failure(ErrorType.ErrUserForbidden, "User is not allowed to access this content");
            }

            var group = _groupRepository.GetGroupById(request.Id);
            if (group == null)
            {
                return ApiResult<List<ExpenseResponse>>.Failure(ErrorType.ErrGroupNotFound, "Group not found, provided group id is invalid");
            }
            var expenses = await _groupRepository.GetGroupExpensesById(request.Id);
            return ApiResult<List<ExpenseResponse>>.Success(expenses);
        }
    }
}
