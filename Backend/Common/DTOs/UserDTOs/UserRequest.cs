using Common.Utilities;
using MediatR;

namespace Common.DTOs.UserDTOs
{
    public class UserRequest : IRequest<ApiResult<UserResponse>>
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
