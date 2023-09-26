﻿using System.Security.Claims;
using Common.Exceptions;
using GroupService.Api.Interfaces;
using GroupService.Api.Models;
using GroupService.Api.Queries;
using MediatR;

namespace GroupService.Api.Handlers
{
    public class AddMemberQueryHandler : IRequestHandler<AddMemberQuery, GroupResponse>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IHttpContextAccessor _contextAccessor;
        public AddMemberQueryHandler(IGroupRepository groupRepository, IHttpContextAccessor contextAccessor)
        {
            _groupRepository = groupRepository;
            _contextAccessor = contextAccessor;
        }

        public async Task<GroupResponse> Handle(AddMemberQuery request, CancellationToken cancellationToken)
        {
            var group = await _groupRepository.GetGroupById(request.GroupId)??throw new GroupNotFoundException("Group is not present");

            var authenticatedUserId = _contextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);

            if (authenticatedUserRole != "Admin" && authenticatedUserId != group.CreatorId.ToString()) {
                throw new UserForbiddenException("User dont have rights to add users in this group");
            }

            try
            {
                var updatedGroup = await _groupRepository.AddMemberInGroup(request.GroupId, request.UserId);
                return updatedGroup;
            }
            catch (UserNotFoundException ex)
            {
                throw ex;
            }
        }
    }
}
