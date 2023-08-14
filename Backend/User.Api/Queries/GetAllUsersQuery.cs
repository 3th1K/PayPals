using MediatR;
using UserService.Api.Models;

namespace UserService.Api.Queries
{
    public record GetAllUsersQuery : IRequest<IEnumerable<UserResponse>>
    {
    }
}
