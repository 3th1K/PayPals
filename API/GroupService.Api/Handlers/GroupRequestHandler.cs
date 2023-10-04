using AutoMapper;
using Common.DTOs.GroupDTOs;
using Common.Exceptions;
using Common.Interfaces;
using Common.Models;
using Common.Utilities;
using MediatR;

namespace GroupService.Api.Handlers
{
    public class GroupRequestHandler : IRequestHandler<GroupRequest, ApiResult<GroupResponse>>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        public GroupRequestHandler(IMapper mapper, IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }
        public async Task<ApiResult<GroupResponse>> Handle(GroupRequest request, CancellationToken cancellationToken)
        {
            Group group = _mapper.Map<Group>(request);
            var checkGrp = await _groupRepository.GetGroupByName(group.GroupName);

            if (checkGrp != null)
            {
                return ApiResult<GroupResponse>.Failure(ErrorType.ErrGroupAlreadyExists,
                    $"Group with name {group.GroupName} is already present");
            }

            var groupCreator = await _userRepository.GetUserById(group.CreatorId);
            if (groupCreator == null)
            {
                return ApiResult<GroupResponse>.Failure(ErrorType.ErrUserNotFound,
                    $"Provided group creator is not a registered user");
            }

            var addedGroup = await _groupRepository.CreateGroup(group);
            return ApiResult<GroupResponse>.Success(addedGroup);
        }
    }
}
