using System.Runtime.CompilerServices;
using Data.Models;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace UserService.Api.Models
{
    public class UserDetailsResponse
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int TotalExpenses { get; set; }

        public bool IsAdmin { get; set; }

        //public virtual ICollection<Approval> Approvals { get; set; } = new List<Approval>();

        
        //public virtual ICollection<ExpenseApproval> ExpenseApprovals { get; set; } = new List<ExpenseApproval>();

        public virtual ICollection<UserDetailsExpenseResponse> Expenses { get; set; } = new List<UserDetailsExpenseResponse>();

        public virtual ICollection<UserDetailsGroupResponse> Groups { get; set; } = new List<UserDetailsGroupResponse>();

        //public virtual ICollection<Expense> ExpensesNavigation { get; set; } = new List<Expense>();

        //public virtual ICollection<Group> GroupsNavigation { get; set; } = new List<Group>();

        
    }
    public class UserDetailsExpenseResponse
    {
        public int ExpenseId { get; set; }

        public int GroupId { get; set; }

        public int PayerId { get; set; }

        public DateTime ExpenseDate { get; set; }

        public decimal Amount { get; set; }

        public int? ApprovalsReceived { get; set; }

        public int TotalMembers { get; set; }

        public string Description { get; set; } = null!;
    }

    public class UserDetailsGroupResponse
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; } = null!;

        public int CreatorId { get; set; }

        public int TotalExpenses { get; set; }
    }


}
