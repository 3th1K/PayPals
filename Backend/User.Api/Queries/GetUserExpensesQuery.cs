using MediatR;
using UserService.Api.Models;

namespace UserService.Api.Queries
{
    public class GetUserExpensesQuery : IRequest<List<ExpenseResponse>>
    {
        public readonly int Id;
        public GetUserExpensesQuery(int id)
        {
            Id = id;
        }
    }
}
