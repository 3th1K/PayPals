using Common.Utilities;
using MediatR;

namespace Common.DTOs.GroupDTOs
{
    public class GroupRequest : IRequest<ApiResult<GroupResponse>>
    {
        public string GroupName { get; set; } = null!;

        public int CreatorId { get; set; }

    }
}
