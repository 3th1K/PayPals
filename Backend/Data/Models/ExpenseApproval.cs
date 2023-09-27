namespace Data.Models;

public partial class ExpenseApproval
{
    public int ExpenseId { get; set; }

    public int UserId { get; set; }

    public bool IsApproved { get; set; }

    public virtual Expense Expense { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}