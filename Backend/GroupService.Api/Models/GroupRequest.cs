using MediatR;

namespace GroupService.Api.Models
{
    public class GroupRequest : IRequest<GroupResponse>
    {
        public string GroupName { get; set; } = null!;

        public int CreatorId { get; set; }

    }
}
