namespace ExpenseService.Api.Models
{
    public class UserForExpenseRequest
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

    }
}
