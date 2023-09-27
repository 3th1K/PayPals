﻿using Common.DTOs.UserDTOs;
using Common.Exceptions;
using Common.Interfaces;
using MediatR;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id) ?? throw new UserNotFoundException("User Is Not Present In Database");
            return user;
        }
    }
}
