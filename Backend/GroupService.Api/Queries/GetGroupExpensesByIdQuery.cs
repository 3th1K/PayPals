using Common.DTOs.ExpenseDTOs;
using Common.Utilities;
using MediatR;

namespace GroupService.Api.Queries
{
    public class GetGroupExpensesByIdQuery : IRequest<ApiResult<List<ExpenseResponse>>>
    {
        public readonly int Id;
        public GetGroupExpensesByIdQuery(int id)
        {

            Id = id;

        }
    }
}
