using MediatR;

namespace Data.DTOs.ExpenseDTOs
{
    public class ExpenseUpdateRequest : IRequest<ExpenseResponse>
    {
        public int ExpenseId { get; set; }

        //public int PayerId { get; set; }

        //public DateTime ExpenseDate { get; set; }

        public decimal Amount { get; set; }

        //public int? ApprovalsReceived { get; set; }

        //public int TotalMembers { get; set; }

        public string Description { get; set; } = null!;

    }
}
