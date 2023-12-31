﻿using PayPals.UI.DTOs.GroupDTOs;
using PayPals.UI.DTOs.UserDTOs;

namespace PayPals.UI.DTOs.ExpenseDTOs
{
    public class ExpenseResponse
    {
        public int ExpenseId { get; set; }

        public int GroupId { get; set; }

        public int PayerId { get; set; }

        public DateTime ExpenseDate { get; set; }

        public decimal Amount { get; set; }

        public int? ApprovalsReceived { get; set; }

        public int TotalMembers { get; set; }

        public string Description { get; set; } = null!;

        //public virtual ICollection<Approval> Approvals { get; set; } = new List<Approval>();

        public virtual ICollection<ExpenseApprovalResponse> ExpenseApprovals { get; set; } = new List<ExpenseApprovalResponse>();

        public virtual GroupResponse Group { get; set; } = null!;

        public virtual UserResponse Payer { get; set; } = null!;

        public virtual ICollection<UserResponse> Users { get; set; } = new List<UserResponse>();

    }
}
