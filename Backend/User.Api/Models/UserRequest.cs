using MediatR;
using LanguageExt.Common;

namespace UserService.Api.Models
{
    public class UserRequest : IRequest<Result<UserResponse>>
    {
        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

    }
}
