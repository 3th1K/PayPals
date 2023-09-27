using Common.DTOs.GroupDTOs;
using MediatR;

namespace GroupService.Api.Queries
{
    public class GetAllGroupsQuery : IRequest<List<GroupResponse>>
    {
    }
}
