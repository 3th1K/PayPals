using LanguageExt.Common;
using MediatR;
using UserService.Api.Interfaces;
using UserService.Api.Models;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class GetUserDetailsByIdQueryHandler : IRequestHandler<GetUserDetailsByIdQuery, Result<User>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserDetailsByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<User>> Handle(GetUserDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserDetailsById(request.Id!);
            return user;
        }
    }
}
