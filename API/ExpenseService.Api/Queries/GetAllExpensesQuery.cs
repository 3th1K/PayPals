using Common.DTOs.ExpenseDTOs;
using Common.Utilities;
using MediatR;

namespace ExpenseService.Api.Queries
{
    public class GetAllExpensesQuery : IRequest<ApiResult<List<ExpenseResponse>>>
    {
    }
}
