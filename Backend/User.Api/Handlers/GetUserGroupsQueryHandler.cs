﻿using LanguageExt.Common;
using MediatR;
using UserService.Api.Interfaces;
using UserService.Api.Models;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class GetUserGroupsQueryHandler : IRequestHandler<GetUserGroupsQuery, List<Group>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserGroupsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<List<Group>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id!) ?? throw new UserNotFoundException();
            var groups = await _userRepository.GetUserGroups(user.UserId);
            return groups;
        }
    }
}