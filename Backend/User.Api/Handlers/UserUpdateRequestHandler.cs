using Common.DTOs.UserDTOs;
using Common.Exceptions;
using Common.Interfaces;
using MediatR;

namespace UserService.Api.Handlers
{
    public class UserUpdateRequestHandler : IRequestHandler<UserUpdateRequest, UserResponse>
    {
        private readonly IUserRepository _userRepository;
        public UserUpdateRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserResponse> Handle(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            var updatedUser = await _userRepository.UpdateUser(request) ?? throw new UserNotFoundException("Cannot Update The User, User Not Present In Db");
            return updatedUser;
        }
    }
}
