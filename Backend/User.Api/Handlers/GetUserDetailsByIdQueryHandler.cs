using Common.Exceptions;
using Data.Models;
using MediatR;
using UserService.Api.Interfaces;
using UserService.Api.Models;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class GetUserDetailsByIdQueryHandler : IRequestHandler<GetUserDetailsByIdQuery, UserDetailsResponse>
    {
        private readonly IUserRepository _userRepository;
        public GetUserDetailsByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDetailsResponse> Handle(GetUserDetailsByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserDetailsById(request.Id) ?? throw new UserNotFoundException("User Is Not Present In Database");
            return user;
        }
    }
}
