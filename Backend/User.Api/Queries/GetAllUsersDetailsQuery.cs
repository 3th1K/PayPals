using MediatR;
using UserService.Api.Models;

namespace UserService.Api.Queries
{
    public record GetAllUsersDetailsQuery : IRequest<IEnumerable<User>>
    {
    }
}
