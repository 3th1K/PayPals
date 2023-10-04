using Common.DTOs.UserDTOs;
using Common.Interfaces;
using Common.Utilities;
using MediatR;

namespace UserService.Api.Handlers
{
    public class UserUpdateRequestHandler : IRequestHandler<UserUpdateRequest, ApiResult<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        public UserUpdateRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ApiResult<UserResponse>> Handle(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            var updatedUser = await _userRepository.UpdateUser(request);
            if (updatedUser == null)
            {
                return ApiResult<UserResponse>.Failure(ErrorType.ErrUserNotFound, "Cannot Update The User, User is not a valid user");
            }

            return ApiResult<UserResponse>.Success(updatedUser);
        }
    }
}
