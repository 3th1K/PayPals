using LanguageExt.Common;
using MediatR;

namespace UserService.Api.Models
{
    public class UserUpdateRequest : IRequest<Result<UserResponse>>
    {
        public int UserId { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? Email { get; set; }

        public int? TotalExpenses { get; set; } = null;

        public bool? IsAdmin { get; set; } = null;
    }
}
