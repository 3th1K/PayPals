using Common.DTOs.GroupDTOs;
using Common.Interfaces;
using Common.Utilities;
using GroupService.Api.Queries;
using MediatR;

namespace GroupService.Api.Handlers
{
    public class GetAllGroupsQueryHandler : IRequestHandler<GetAllGroupsQuery, ApiResult<List<GroupResponse>>>
    {
        private readonly IGroupRepository _groupRepository;
        public GetAllGroupsQueryHandler(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }
        public async Task<ApiResult<List<GroupResponse>>> Handle(GetAllGroupsQuery request, CancellationToken cancellationToken)
        {
            var groups = await _groupRepository.GetAllGroups();
            return ApiResult<List<GroupResponse>>.Success(groups);
        }
    }
}
