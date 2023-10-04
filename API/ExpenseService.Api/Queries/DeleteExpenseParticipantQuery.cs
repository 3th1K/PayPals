using Common.DTOs.ExpenseDTOs;
using Common.Utilities;
using MediatR;

namespace ExpenseService.Api.Queries;

public class DeleteExpenseParticipantQuery : IRequest<ApiResult<ExpenseResponse>>
{
    public readonly int ExpenseId;
    public readonly int UserId;

    public DeleteExpenseParticipantQuery(int expenseId, int userId)
    {
        ExpenseId = expenseId;
        UserId = userId;
    }
}