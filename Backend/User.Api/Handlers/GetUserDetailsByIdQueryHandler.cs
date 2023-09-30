using Common.DTOs.UserDTOs;
using Common.Interfaces;
using Common.Utilities;
using MediatR;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class GetUserDetailsByIdQueryHandler : IRequestHandler<GetUserDetailsByIdQuery, ApiResult<UserDetailsResponse>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserDetailsByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ApiResult<UserDetailsResponse>> Handle(GetUserDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserDetailsById(request.Id);
            if (user == null)
            {
                return ApiResult<UserDetailsResponse>.Failure(ErrorType.ErrUserNotFound,"User is not a valid user");
            }

            return ApiResult<UserDetailsResponse>.Success(user);
        }
    }
}
