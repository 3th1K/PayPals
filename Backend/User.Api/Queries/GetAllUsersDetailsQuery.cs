using Data.Models;
using MediatR;

namespace UserService.Api.Queries
{
    public record GetAllUsersDetailsQuery : IRequest<IEnumerable<User>>;
}
