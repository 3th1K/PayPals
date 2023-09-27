namespace Data.DTOs.UserDTOs
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
}
