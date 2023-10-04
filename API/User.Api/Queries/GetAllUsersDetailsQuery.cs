using Common.DTOs.UserDTOs;
using MediatR;

namespace UserService.Api.Queries
{
    public record GetAllUsersDetailsQuery : IRequest<IEnumerable<UserDetailsResponse>>;
}
