using MediatR;

namespace Data.DTOs.GroupDTOs
{
    public class GroupRequest : IRequest<GroupResponse>
    {
        public string GroupName { get; set; } = null!;

        public int CreatorId { get; set; }

    }
}
