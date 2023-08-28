using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace UserService.Api.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int TotalExpenses { get; set; }

    public bool IsAdmin { get; set; }
    
    [JsonIgnore]

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    [JsonIgnore]
    public virtual ICollection<Group> GroupsNavigation { get; set; } = new List<Group>();
}
