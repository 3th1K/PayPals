using System;
using System.Collections.Generic;

namespace Data.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int TotalExpenses { get; set; }

    public bool IsAdmin { get; set; }

    public virtual ICollection<Approval> Approvals { get; set; } = new List<Approval>();

    public virtual ICollection<ExpenseApproval> ExpenseApprovals { get; set; } = new List<ExpenseApproval>();

    public virtual ICollection<Expense> Expenses { get; set; } = new List<Expense>();

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();

    public virtual ICollection<Expense> ExpensesNavigation { get; set; } = new List<Expense>();

    public virtual ICollection<Group> GroupsNavigation { get; set; } = new List<Group>();
}
