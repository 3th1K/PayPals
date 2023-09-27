using Common.DTOs.UserDTOs;

namespace Common.DTOs.ExpenseDTOs;

public class ExpenseApprovalResponse
{
    public int UserId { get; set; }

    public bool IsApproved { get; set; }

    public virtual UserResponse User { get; set; } = null!;
}