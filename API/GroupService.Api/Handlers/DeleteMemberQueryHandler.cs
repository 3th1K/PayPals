using System.Security.Claims;
using Common.DTOs.GroupDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using Data.Repositories;
using GroupService.Api.Queries;
using MediatR;

namespace GroupService.Api.Handlers
{
    public class DeleteMemberQueryHandler : IRequestHandler<DeleteMemberQuery, ApiResult<GroupResponse>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IUserRepository _userRepository;
        public DeleteMemberQueryHandler(IGroupRepository groupRepository, IHttpContextAccessor contextAccessor, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _contextAccessor = contextAccessor;
            _userRepository = userRepository;
        }

        public async Task<ApiResult<GroupResponse>> Handle(DeleteMemberQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupById(request.GroupId) ?? throw new GroupNotFoundException("Group is not present");

            var authenticatedUserId = _contextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            if (authenticatedUserRole != "Admin" && authenticatedUserId != group.CreatorId.ToString())
            {
                return ApiResult<GroupResponse>.Failure(ErrorType.ErrUserForbidden, "User do not have rights to add users in this group");
            }
            var user = await _userRepository.GetUserById(request.UserId);
            if (user == null)
            {
                ApiResult<GroupResponse>.Failure(ErrorType.ErrUserNotFound, "Provided user does not exists");
            }

            var updatedGroup = await _groupRepository.DeleteMemberFromGroup(request.GroupId, request.UserId);
            return ApiResult<GroupResponse>.Success(updatedGroup);
        }
    }
}
