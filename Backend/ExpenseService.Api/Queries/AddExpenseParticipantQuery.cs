using Common.DTOs.ExpenseDTOs;
using Common.Utilities;
using MediatR;

namespace ExpenseService.Api.Queries;

public class AddExpenseParticipantQuery : IRequest<ApiResult<ExpenseResponse>>
{
    public readonly int ExpenseId;
    public readonly int UserId;

    public AddExpenseParticipantQuery(int expenseId, int userId)
    {
        ExpenseId = expenseId;
        UserId = userId;
    }
}