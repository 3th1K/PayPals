using AutoMapper;
using LanguageExt.Common;
using MediatR;
using System.ComponentModel.DataAnnotations;
using UserService.Api.Interfaces;
using UserService.Api.Models;

namespace UserService.Api.Handlers
{
    public class UserRequestHandlers : IRequestHandler<UserRequest, Result<UserResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserRequestHandlers(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<Result<UserResponse>> Handle(UserRequest request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);
            var addedUser = await _userRepository.CreateUser(user);
            return addedUser;
        }
    }
}
