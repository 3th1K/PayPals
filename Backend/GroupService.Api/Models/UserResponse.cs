namespace GroupService.Api.Models
{
    public class UserResponse
    {
        public int UserId { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int TotalExpenses { get; set; }
    }
}
