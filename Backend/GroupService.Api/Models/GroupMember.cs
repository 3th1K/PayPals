using Data.Models;
using System;
using System.Collections.Generic;

namespace GroupService.Api.Models;

public partial class GroupMember
{
    public int GroupId { get; set; }

    public int UserId { get; set; }

    public virtual Group Group { get; set; } = null!;
}
