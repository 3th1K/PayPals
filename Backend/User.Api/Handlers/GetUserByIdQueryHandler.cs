using LanguageExt.Common;
using MediatR;
using UserService.Api.Interfaces;
using UserService.Api.Models;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserResponse>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id!);
            return user;
        }
    }
}
