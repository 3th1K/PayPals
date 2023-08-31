using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ExpenseService.Api.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int TotalExpenses { get; set; }

    public bool IsAdmin { get; set; }

    [JsonIgnore]
    public virtual ICollection<Approval> Approvals { get; set; } = new List<Approval>();

    [JsonIgnore]
    public virtual ICollection<ExpenseApproval> ExpenseApprovals { get; set; } = new List<ExpenseApproval>();

    [JsonIgnore]
    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    [JsonIgnore]
    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    [JsonIgnore]
    public virtual ICollection<Expense> ExpensesNavigation { get; set; } = new List<Expense>();

    [JsonIgnore]
    public virtual ICollection<Group> GroupsNavigation { get; set; } = new List<Group>();
}
