using Common.DTOs.ExpenseDTOs;
using Common.Utilities;
using MediatR;

namespace UserService.Api.Queries
{
    public class GetUserExpensesQuery : IRequest<ApiResult<List<ExpenseResponse>>>
    {
        public readonly int Id;
        public GetUserExpensesQuery(int id)
        {
            Id = id;
        }
    }
}
