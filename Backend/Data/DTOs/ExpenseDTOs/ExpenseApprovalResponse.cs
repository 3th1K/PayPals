using Data.Models;

namespace Data.DTOs.ExpenseDTOs;

public class ExpenseApprovalResponse
{
    public int UserId { get; set; }

    public bool IsApproved { get; set; }

    public virtual User User { get; set; } = null!;
}