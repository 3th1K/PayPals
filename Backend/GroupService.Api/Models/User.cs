using System;
using System.Collections.Generic;

namespace GroupService.Api.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int TotalExpenses { get; set; }

    public bool IsAdmin { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Group> GroupsNavigation { get; set; } = new List<Group>();
}
