using Common.DTOs.ExpenseDTOs;
using Common.Utilities;
using MediatR;

namespace ExpenseService.Api.Queries;

public class GetExpenseStatusByIdQuery : IRequest<ApiResult<ExpenseStatusResponse>>
{
    public readonly int Id;
    public GetExpenseStatusByIdQuery(int id)
    {
        Id = id;
    }
}