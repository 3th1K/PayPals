using MediatR;

namespace Common.DTOs.UserDTOs
{
    public class UserRequest : IRequest<UserResponse>
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
