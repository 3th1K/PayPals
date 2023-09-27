using Data.DTOs.GroupDTOs;
using MediatR;

namespace GroupService.Api.Queries
{
    public class DeleteGroupQuery : IRequest<GroupResponse>
    {
        public readonly int GroupId;
        public DeleteGroupQuery(int id)
        {
            GroupId = id;
        }
    }
}
