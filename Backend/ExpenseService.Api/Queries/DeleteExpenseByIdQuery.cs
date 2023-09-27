using Data.DTOs.ExpenseDTOs;
using MediatR;

namespace ExpenseService.Api.Queries
{
    public class DeleteExpenseByIdQuery : IRequest<ExpenseResponse>
    {
        public readonly int Id;
        public DeleteExpenseByIdQuery(int id)
        {
            Id = id;
        }
    }
}
