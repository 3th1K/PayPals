using Common.DTOs.GroupDTOs;
using Common.Utilities;
using MediatR;

namespace GroupService.Api.Queries
{
    public class DeleteMemberQuery : IRequest<ApiResult<GroupResponse>>
    {
        public readonly int GroupId;
        public readonly int UserId;
        public DeleteMemberQuery(int groupId, int userId)
        {

            GroupId = groupId;
            UserId = userId;

        }
    }
}
