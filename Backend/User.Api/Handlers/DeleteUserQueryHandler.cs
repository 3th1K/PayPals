using Common.Exceptions;
using Common.Interfaces;
using Common.Models;
using Common.Utilities;
using MediatR;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class DeleteUserQueryHandler : IRequestHandler<DeleteUserQuery, ApiResult<User>>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ApiResult<User>> Handle(DeleteUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.DeleteUser(request.Id);
            if (user != null)
            {
                return ApiResult<User>.Success(user);
            }
            return ApiResult<User>.Failure(ErrorType.ErrUserNotFound, "User do not exists");
        }
    }
}
