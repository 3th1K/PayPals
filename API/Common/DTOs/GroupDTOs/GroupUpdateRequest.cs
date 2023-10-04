using Common.Utilities;
using MediatR;

namespace Common.DTOs.GroupDTOs
{
    public class GroupUpdateRequest : IRequest<ApiResult<GroupResponse>>
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;
        public int CreatorId { get; set; }
        public int TotalExpenses { get; set; }
    }
}
