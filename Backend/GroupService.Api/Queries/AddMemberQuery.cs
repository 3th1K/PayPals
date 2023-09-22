using GroupService.Api.Models;
using MediatR;

namespace GroupService.Api.Queries
{
    public class AddMemberQuery : IRequest<GroupResponse>
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
