namespace Common.Models;

public partial class Approval
{
    public int ApprovalId { get; set; }

    public int ExpenseId { get; set; }

    public int ApproverId { get; set; }

    public int ApprovalStatus { get; set; }

    public virtual User Approver { get; set; } = null!;

    public virtual Expense Expense { get; set; } = null!;
}
