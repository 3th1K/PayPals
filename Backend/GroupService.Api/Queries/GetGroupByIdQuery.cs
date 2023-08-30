using GroupService.Api.Models;
using MediatR;

namespace GroupService.Api.Queries
{
    public class GetGroupByIdQuery : IRequest<GroupResponse>
    {
        public readonly int Id;
        public GetGroupByIdQuery(int id)
        {

            Id = id;

        }
    }
}
