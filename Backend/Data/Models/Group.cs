using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Data.Models;

public partial class Group
{
    public int GroupId { get; set; }

    public string GroupName { get; set; } = null!;

    public int CreatorId { get; set; }

    public int TotalExpenses { get; set; }

    [JsonIgnore]
    public User Creator { get; set; } = null!;

    //[JsonIgnore]
    public ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    //[JsonIgnore]
    public ICollection<User> Users { get; set; } = new List<User>();
}
