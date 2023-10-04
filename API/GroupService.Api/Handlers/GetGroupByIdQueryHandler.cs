using System.Security.Claims;
using Common.DTOs.GroupDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using GroupService.Api.Queries;
using MediatR;

namespace GroupService.Api.Handlers
{
    public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, ApiResult<GroupResponse>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetGroupByIdQueryHandler(IGroupRepository groupRepository, IHttpContextAccessor httpContextAccessor)
        {
            _groupRepository = groupRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<ApiResult<GroupResponse>> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole =_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            var group = await _groupRepository.GetGroupById(request.Id);
            if (group == null)
            {
                return ApiResult<GroupResponse>.Failure(ErrorType.ErrGroupNotFound, "Group not found, Invalid Group");
            }
            if (
                authenticatedUserRole != "Admin" && 
                !await _groupRepository.CheckUserExistenceInGroup(request.Id, int.Parse(authenticatedUserId!))
            )
            {
                return ApiResult<GroupResponse>.Failure(ErrorType.ErrUserForbidden, "User does not have access to this content");
            }
            return ApiResult<GroupResponse>.Success(group);
        }
    }
}
