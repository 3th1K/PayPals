using MediatR;
using UserService.Api.Interfaces;
using UserService.Api.Models;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class GetAllUsersDetailsQueryHandler : IRequestHandler<GetAllUsersDetailsQuery, IEnumerable<User>>
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersDetailsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<User>> Handle(GetAllUsersDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetAllUsersDetails();
        }
    }
}
