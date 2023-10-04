using Common.Models;
using Common.Utilities;
using MediatR;

namespace UserService.Api.Queries
{
    public class GetUserGroupsQuery : IRequest<ApiResult<List<Group>>>
    {
        public readonly int Id;
        public GetUserGroupsQuery(int id)
        {
            Id = id;
        }
    }
}
