using Common.Interfaces;
using Common.Models;
using Common.Utilities;
using MediatR;
using UserService.Api.Queries;

namespace UserService.Api.Handlers
{
    public class GetUserGroupsQueryHandler : IRequestHandler<GetUserGroupsQuery, ApiResult<List<Group>>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserGroupsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ApiResult<List<Group>>> Handle(GetUserGroupsQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserById(request.Id);

            if (user == null)
            {
                return ApiResult<List<Group>>.Failure(ErrorType.ErrUserNotFound, "This User is not a valid User");
            }
            var groups = await _userRepository.GetUserGroups(request.Id);
            return ApiResult<List<Group>>.Success(groups);
        }
    }
}
