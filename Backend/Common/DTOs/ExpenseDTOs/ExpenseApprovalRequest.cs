using System.Text.Json.Serialization;
using Common.Utilities;
using MediatR;

namespace Common.DTOs.ExpenseDTOs;

public class ExpenseApprovalRequest : IRequest<ApiResult<ExpenseResponse>>
{
    [JsonIgnore] public int ExpenseId { get; set; } = 0;
    public int UserId { get; set; }

    public bool? IsApproved { get; set; } = null;
}