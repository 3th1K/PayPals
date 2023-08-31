using MediatR;

namespace ExpenseService.Api.Models
{
    public class ExpenseRequest : IRequest<ExpenseResponse>
    {
        public int GroupId { get; set; }

        public int PayerId { get; set; }

        public DateTime ExpenseDate { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; } = null!;

        public virtual ICollection<int> UserIds { get; set; } = new List<int>();

    }
}
