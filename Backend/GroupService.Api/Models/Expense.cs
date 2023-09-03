﻿using System;
using System.Collections.Generic;

namespace GroupService.Api.Models;

public partial class Expense
{
    public int ExpenseId { get; set; }

    public int GroupId { get; set; }

    public int PayerId { get; set; }

    public DateTime ExpenseDate { get; set; }

    public decimal Amount { get; set; }

    public int? ApprovalsReceived { get; set; }

    public int TotalMembers { get; set; }

    public string Description { get; set; } = null!;

    public virtual ICollection<Approval> Approvals { get; set; } = new List<Approval>();

    public virtual ICollection<ExpenseApproval> ExpenseApprovals { get; set; } = new List<ExpenseApproval>();

    public virtual Group Group { get; set; } = null!;

    public virtual User Payer { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
