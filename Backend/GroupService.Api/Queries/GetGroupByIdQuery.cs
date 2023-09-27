using Common.DTOs.GroupDTOs;
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
