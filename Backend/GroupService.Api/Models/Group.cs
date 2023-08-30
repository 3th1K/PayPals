using System;
using System.Collections.Generic;

namespace GroupService.Api.Models;

public partial class Group
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public int CreatorId { get; set; }

    public int TotalExpenses { get; set; }

    public virtual User Creator { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
