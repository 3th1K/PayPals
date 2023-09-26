using Data.Models;
using ExpenseService.Api.Models;
using MediatR;

namespace ExpenseService.Api.Queries
{
    public class GetAllExpensesQuery : IRequest<List<ExpenseResponse>>
    {
    }
}
