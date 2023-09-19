using Common.Exceptions;
using Data.Models;
using LanguageExt.Common;
using MediatR;
using UserService.Api.Interfaces;
using UserService.Api.Models;
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
            return await _userRepository.DeleteUser(request.Id) ?? throw new UserNotFoundException("User Dosent Exists");
        }
    }
}
