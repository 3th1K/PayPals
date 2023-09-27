using System.Text.Json.Serialization;
using MediatR;

namespace Common.DTOs.ExpenseDTOs;

public class ExpenseApprovalRequest : IRequest<ExpenseResponse>
{
    [JsonIgnore] public int ExpenseId { get; set; } = 0;
    public int UserId { get; set; }

    public bool? IsApproved { get; set; } = null;
}