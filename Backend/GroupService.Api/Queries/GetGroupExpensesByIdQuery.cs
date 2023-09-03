using GroupService.Api.Models;
using MediatR;

namespace GroupService.Api.Queries
{
    public class GetGroupExpensesByIdQuery : IRequest<List<ExpenseResponse>>
    {
        public readonly int Id;
        public GetGroupExpensesByIdQuery(int id)
        {

            Id = id;

        }
    }
}
