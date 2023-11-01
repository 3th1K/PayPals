namespace PayPals.UI.DTOs.ExpenseDTOs
{
    public class ExpenseRequest
    {
        public int GroupId { get; set; }

        public int PayerId { get; set; }

        public DateTime ExpenseDate { get; set; } = DateTime.Now;

        public decimal Amount { get; set; }

        public string Description { get; set; } = null!;

        public virtual ICollection<int> UserIds { get; set; } = new List<int>();

    }
}
