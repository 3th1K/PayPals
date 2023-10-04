using Common.DTOs.ExpenseDTOs;
using Common.Utilities;
using MediatR;

namespace ExpenseService.Api.Queries
{
    public class DeleteExpenseByIdQuery : IRequest<ApiResult<ExpenseResponse>>
    {
        public readonly int Id;
        public DeleteExpenseByIdQuery(int id)
        {
            Id = id;
        }
    }
}
