using AutoMapper;
using Common.Exceptions;
using Data.Models;
using MediatR;
using UserService.Api.Interfaces;
using UserService.Api.Models;

namespace UserService.Api.Handlers
{
    public class UserRequestHandlers : IRequestHandler<UserRequest, UserResponse>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserRequestHandlers(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<UserResponse> Handle(UserRequest request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);

            var existingUser = await _userRepository.GetUserByUsernameOrEmail(user.Username, user.Email);
            if (existingUser != null)
            {
                throw new UserAlreadyExistsException("User already exists with the provided username or email");
            }

            var addedUser = await _userRepository.CreateUser(user);
            return addedUser;
        }
    }
}
