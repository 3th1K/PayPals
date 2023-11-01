namespace PayPals.UI.DTOs.ExpenseDTOs;

public class ExpenseApprovalRequest
{
    public int ExpenseId { get; set; } = 0;
    public int UserId { get; set; }

    public bool? IsApproved { get; set; } = null;
}