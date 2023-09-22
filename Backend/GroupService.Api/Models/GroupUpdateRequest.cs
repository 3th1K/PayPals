using MediatR;

namespace GroupService.Api.Models
{
    public class GroupUpdateRequest : IRequest<GroupResponse>
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public int CreatorId { get; set; }
        public int TotalExpenses { get; set; }
    }
}
