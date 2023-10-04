using AutoMapper;
using Common.DTOs.UserDTOs;
using Common.Interfaces;
using Common.Models;
using Common.Utilities;
using MediatR;

namespace UserService.Api.Handlers
{
    public class UserRequestHandlers : IRequestHandler<UserRequest, ApiResult<UserResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserRequestHandlers(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<ApiResult<UserResponse>> Handle(UserRequest request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);

            var existingUser = await _userRepository.GetUserByUsernameOrEmail(user.Username, user.Email);
            if (existingUser != null)
            {
                return ApiResult<UserResponse>.Failure(ErrorType.ErrApprovalAlreadyExists, "User already exists with the provided username or email");
            }

            var addedUser = await _userRepository.CreateUser(user);
            return ApiResult<UserResponse>.Success(addedUser);
        }
    }
}
