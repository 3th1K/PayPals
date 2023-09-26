using Data.Models;
using MediatR;

namespace UserService.Api.Queries
{
    public class GetUserGroupsQuery : IRequest<List<Group>>
    {
        public readonly int Id;
        public GetUserGroupsQuery(int id)
        {
            Id = id;
        }
    }
}
