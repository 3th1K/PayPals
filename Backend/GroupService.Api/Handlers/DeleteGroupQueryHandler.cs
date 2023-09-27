using System.Security.Claims;
using Common.Exceptions;
using Data.DTOs.GroupDTOs;
using GroupService.Api.Interfaces;
using GroupService.Api.Queries;
using MediatR;

namespace GroupService.Api.Handlers
{
    public class DeleteGroupQueryHandler : IRequestHandler<DeleteGroupQuery, GroupResponse>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DeleteGroupQueryHandler(IGroupRepository groupRepository, IHttpContextAccessor contextAccessor)
        {
            _groupRepository = groupRepository;
            _httpContextAccessor = contextAccessor;

        }
        public async Task<GroupResponse> Handle(DeleteGroupQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupById(request.GroupId)?? throw new GroupNotFoundException("Unable to delete group");

            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            if (authenticatedUserRole != "Admin" && authenticatedUserId != group.CreatorId.ToString())
            {
                throw new UserForbiddenException("User is not allowed to delete this group");
            }

            var groupResponse = await _groupRepository.DeleteGroup(request.GroupId)?? throw new GroupNotFoundException("Group Does Not Exists");
            return groupResponse;
        }
    }
}
