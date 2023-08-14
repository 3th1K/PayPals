using AutoMapper;
using LanguageExt.Common;
using MediatR;
using UserService.Api.Interfaces;
using UserService.Api.Models;

namespace UserService.Api.Handlers
{
    public class UserUpdateRequestHandler : IRequestHandler<UserUpdateRequest, Result<UserResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserUpdateRequestHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<Result<UserResponse>> Handle(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            var addedUser = await _userRepository.UpdateUser(request);
            return addedUser;
        }
    }
}
