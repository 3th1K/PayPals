using MediatR;

namespace Data.DTOs.UserDTOs
{
    public class UserUpdateRequest : IRequest<UserResponse>
    {
        public int UserId { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public int? TotalExpenses { get; set; } = null;

        public bool? IsAdmin { get; set; } = null;
    }
}
