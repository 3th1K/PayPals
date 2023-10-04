using System.Security.Claims;
using Common.DTOs.GroupDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Utilities;
using MediatR;

namespace GroupService.Api.Handlers
{
    public class GroupUpdateRequestHandler : IRequestHandler<GroupUpdateRequest, ApiResult<GroupResponse>>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserRepository _userRepository;
        public GroupUpdateRequestHandler(IGroupRepository groupRepository, IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }
        public async Task<ApiResult<GroupResponse>> Handle(GroupUpdateRequest request, CancellationToken cancellationToken)
        {
            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            var groupResponse = await _groupRepository.GetGroupById(request.GroupId);
            if (authenticatedUserRole != "Admin" && groupResponse.CreatorId.ToString() != authenticatedUserId)
            {
                return ApiResult<GroupResponse>.Failure(ErrorType.ErrUserForbidden,"User is not allowed to access this content");
            }

            if (groupResponse == null)
            {
                return ApiResult<GroupResponse>.Failure(ErrorType.ErrGroupNotFound, "Cannot Update The Group, Group is not valid");
            }

            var creator = await _userRepository.GetUserById(request.CreatorId);
            if (creator == null)
            {
                return ApiResult<GroupResponse>.Failure(ErrorType.ErrUserNotFound, "Provided group creator is not a registered user");
            }

            var updatedGroup = await _groupRepository.UpdateGroup(request);
            return ApiResult<GroupResponse>.Success(updatedGroup);

            //if (authenticatedUserRole != "Admin")
            //{
            //    var groupResponse = await _groupRepository.GetGroupById(request.GroupId);
            //    if (groupResponse != null)
            //    {
            //        if (groupResponse.CreatorId.ToString() != authenticatedUserId) 
            //        {
            //            throw new UserForbiddenException("User is not allowed to access this content");
            //        }
            //        try
            //        {
            //            var updatedGroup = await _groupRepository.UpdateGroup(request) ?? throw new GroupNotFoundException("Cannot Update The Group, Group Not Present In Db");
            //            return updatedGroup;
            //        }
            //        catch (UserNotFoundException ex) { throw; }
            //    }
            //    throw new UserForbiddenException("User is not allowed to access this content");
            //}
            //try
            //{
            //    var updatedGroup = await _groupRepository.UpdateGroup(request) ?? throw new GroupNotFoundException("Cannot Update The Group, Group Not Present In Db");
            //    return updatedGroup;
            //}
            //catch (UserNotFoundException ex) { throw; }
        }
    }
}
