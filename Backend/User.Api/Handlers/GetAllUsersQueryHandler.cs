using Common.DTOs.UserDTOs;
using Common.Interfaces;
using Common.Utilities;
using MediatR;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ApiResult<List<UserResponse>>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ApiResult<List<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsers();
            return ApiResult<List<UserResponse>>.Success(users);
        }
    }
}
