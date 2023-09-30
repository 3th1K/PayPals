using Common.DTOs.GroupDTOs;
using Common.Utilities;
using MediatR;

namespace GroupService.Api.Queries
{
    public class AddMemberQuery : IRequest<ApiResult<GroupResponse>>
    {
        public readonly int GroupId;
        public readonly int UserId;
        public AddMemberQuery(int groupId, int userId)
        {

            GroupId = groupId;
            UserId = userId;

        }
    }
}
