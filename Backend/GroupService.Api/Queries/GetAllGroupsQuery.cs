using GroupService.Api.Models;
using MediatR;

namespace GroupService.Api.Queries
{
    public class GetAllGroupsQuery : IRequest<List<GroupResponse>>
    {
    }
}
