using Common.Exceptions;
using GroupService.Api.Interfaces;
using GroupService.Api.Models;
using MediatR;
using System.Security.Claims;

namespace GroupService.Api.Handlers
{
    public class GroupUpdateRequestHandler : IRequestHandler<GroupUpdateRequest, GroupResponse>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GroupUpdateRequestHandler(IGroupRepository groupRepository, IHttpContextAccessor httpContextAccessor)
        {
            _groupRepository = groupRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<GroupResponse> Handle(GroupUpdateRequest request, CancellationToken cancellationToken)
        {
            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            if (authenticatedUserRole != "Admin")
            {
                var groupResponse = await _groupRepository.GetGroupById(request.GroupId);
                if (groupResponse != null)
                {
                    if (groupResponse.CreatorId.ToString() != authenticatedUserId) 
                    {
                        throw new UserForbiddenException("User is not allowed to access this content");
                    }
                    try
                    {
                        var updatedGroup = await _groupRepository.UpdateGroup(request) ?? throw new GroupNotFoundException("Cannot Update The Group, Group Not Present In Db");
                        return updatedGroup;
                    }
                    catch (UserNotFoundException ex) { throw ex; }
                    catch (Exception) { throw; }

                }
                throw new UserForbiddenException("User is not allowed to access this content");
            }
            try
            {
                var updatedGroup = await _groupRepository.UpdateGroup(request) ?? throw new GroupNotFoundException("Cannot Update The Group, Group Not Present In Db");
                return updatedGroup;
            }
            catch (UserNotFoundException ex) { throw ex; }
            catch (Exception) { throw; }
        }
    }
}
