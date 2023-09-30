using System.Security.Claims;
using Common.DTOs.GroupDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using GroupService.Api.Queries;
using MediatR;

namespace GroupService.Api.Handlers
{
    public class DeleteGroupQueryHandler : IRequestHandler<DeleteGroupQuery, ApiResult<GroupResponse>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeleteGroupQueryHandler(IGroupRepository groupRepository, IHttpContextAccessor contextAccessor)
        {
            _groupRepository = groupRepository;
            _httpContextAccessor = contextAccessor;

        }
        public async Task<ApiResult<GroupResponse>> Handle(DeleteGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupById(request.GroupId);
            if (group == null)
            {
                return ApiResult<GroupResponse>.Failure(ErrorType.ErrGroupNotFound, "Unable to delete group");
            }

            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            if (authenticatedUserRole != "Admin" && authenticatedUserId != group.CreatorId.ToString())
            {
                return ApiResult<GroupResponse>.Failure(ErrorType.ErrUserForbidden, "User is not allowed to delete this group");
            }

            var groupResponse = await _groupRepository.DeleteGroup(request.GroupId);
            return ApiResult<GroupResponse>.Success(groupResponse);
        }
    }
}
