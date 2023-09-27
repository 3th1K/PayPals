using Data.DTOs.ExpenseDTOs;
using MediatR;

namespace ExpenseService.Api.Queries
{
    public class GetAllExpensesQuery : IRequest<List<ExpenseResponse>>
    {
    }
}
