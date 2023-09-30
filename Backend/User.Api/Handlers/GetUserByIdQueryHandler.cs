using Common.DTOs.UserDTOs;
using Common.Interfaces;
using Common.Utilities;
using MediatR;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResult<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ApiResult<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);
            if (user == null)
            {
                return ApiResult<UserResponse>.Failure(ErrorType.ErrUserNotFound, "User Is Not Present In Database");
            }
            
            return ApiResult<UserResponse>.Success(user);
        }
    }
}
