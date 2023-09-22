using Data.Models;

namespace GroupService.Api.Models;

public partial class GroupMember
{
    //public int GroupId { get; set; } = 0;
    public int UserId { get; set; }

    //public virtual Group Group { get; set; } = null!;
}
