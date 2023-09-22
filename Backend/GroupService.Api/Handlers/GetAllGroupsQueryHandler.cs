using GroupService.Api.Interfaces;
using GroupService.Api.Models;
using GroupService.Api.Queries;
using MediatR;

namespace GroupService.Api.Handlers
{
    public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, List<GroupResponse>>
    {
        private readonly IGroupRepository _groupRepository;
        public GetAllGroupsQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<List<GroupResponse>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
        {
            return await _groupRepository.GetAllGroups();
        }
    }
}
