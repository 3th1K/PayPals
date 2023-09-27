﻿using System.Security.Claims;
using Common.Exceptions;
using Data.DTOs.GroupDTOs;
using GroupService.Api.Interfaces;
using GroupService.Api.Queries;
using MediatR;

namespace GroupService.Api.Handlers
{
    public class GetGroupByIdQueryHandler : IRequestHandler<GetGroupByIdQuery, GroupResponse>
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public GetGroupByIdQueryHandler(IGroupRepository groupRepository, IHttpContextAccessor httpContextAccessor)
        {
            _groupRepository = groupRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<GroupResponse> Handle(GetGroupByIdQuery request, CancellationToken cancellationToken)
        {
            var authenticatedUserId = _httpContextAccessor.HttpContext?.User.FindFirstValue("userId");
            var authenticatedUserRole =_httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            var group = await _groupRepository.GetGroupById(request.Id) ?? throw new GroupNotFoundException();
            if (
                authenticatedUserRole != "Admin" && 
                !await _groupRepository.CheckUserExistenceInGroup(request.Id, int.Parse(authenticatedUserId!))
            )
            {
                throw new UserNotAuthorizedException();
            }
            return group;
        }
    }
}
