using ExpenseService.Api.Models;
using MediatR;

namespace ExpenseService.Api.Queries
{
    public class GetExpenseDetailsByIdQuery : IRequest<Expense>
    {
        public readonly int Id;
        public GetExpenseDetailsByIdQuery(int id) 
        {
            Id = id;
        }
    }
}
