using Data.Models;
using LanguageExt.Common;
using MediatR;
using UserService.Api.Models;

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
