using MediatR;
using Newtonsoft.Json;

namespace Data.DTOs.ExpenseDTOs;

public class ExpenseApprovalRequest : IRequest<ExpenseResponse>
{
    [JsonIgnore] public int ExpenseId { get; set; } = 0;
    public int UserId { get; set; }

    public bool IsApproved { get; set; }
}