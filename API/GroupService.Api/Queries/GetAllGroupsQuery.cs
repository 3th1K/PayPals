using Common.DTOs.GroupDTOs;
using Common.Utilities;
using MediatR;

namespace GroupService.Api.Queries
{
    public class GetAllGroupsQuery : IRequest<ApiResult<List<GroupResponse>>>
    {
    }
}
