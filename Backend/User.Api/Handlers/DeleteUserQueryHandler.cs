using Common.Exceptions;
using Common.Interfaces;
using Common.Models;
using MediatR;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class DeleteUserQueryHandler : IRequestHandler<DeleteUserQuery, User>
    {
        private readonly IUserRepository _userRepository;
        public DeleteUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> Handle(DeleteUserQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.DeleteUser(request.Id) ?? throw new UserNotFoundException("User Does not Exists");
        }
    }
}
