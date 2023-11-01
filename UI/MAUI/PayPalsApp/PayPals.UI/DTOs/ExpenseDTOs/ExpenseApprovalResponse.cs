using PayPals.UI.DTOs.UserDTOs;

namespace PayPals.UI.DTOs.ExpenseDTOs;

public class ExpenseApprovalResponse
{
    public int UserId { get; set; }

    public bool IsApproved { get; set; }

    public virtual UserResponse User { get; set; } = null!;
}