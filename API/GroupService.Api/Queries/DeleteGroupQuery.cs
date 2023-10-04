using Common.DTOs.GroupDTOs;
using Common.Utilities;
using MediatR;

namespace GroupService.Api.Queries
{
    public class DeleteGroupQuery : IRequest<ApiResult<GroupResponse>>
    {
        public readonly int GroupId;
        public DeleteGroupQuery(int id)
        {
            GroupId = id;
        }
    }
}
