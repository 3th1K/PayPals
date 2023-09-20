using AutoMapper;
using Common.Exceptions;
using Data.Models;
using GroupService.Api.Interfaces;
using GroupService.Api.Models;
using MediatR;

namespace GroupService.Api.Handlers
{
    public class GroupRequestHandler : IRequestHandler<GroupRequest, GroupResponse>
    {
        private readonly IMapper _mapper;
        private readonly IGroupRepository _groupRepository;
        public GroupRequestHandler(IMapper mapper, IGroupRepository groupRepository)
        {
            _mapper = mapper;
            _groupRepository = groupRepository;
        }
        public async Task<GroupResponse> Handle(GroupRequest request, CancellationToken cancellationToken)
        {
            Group group = _mapper.Map<Group>(request);
            var checkGrp = await _groupRepository.GetGroupByName(group.GroupName);

            if(checkGrp != null)
                throw new GroupAlreadyExistsException($"Group with name {group.GroupName} is already present");
            try
            {
                var addedGroup = await _groupRepository.CreateGroup(group);
                return addedGroup;
            }
            catch (UserNotFoundException ex)
            {
                throw ex;
            }
            catch (Exception) { throw; }
            
        }
    }
}
