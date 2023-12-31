﻿using Common.DTOs.UserDTOs;
using Common.Interfaces;
using MediatR;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class GetAllUsersDetailsQueryHandler : IRequestHandler<GetAllUsersDetailsQuery, IEnumerable<UserDetailsResponse>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersDetailsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<UserDetailsResponse>> Handle(GetAllUsersDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllUsersDetails();
        }
    }
}
