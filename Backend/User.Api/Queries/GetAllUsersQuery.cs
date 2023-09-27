using Data.DTOs.UserDTOs;
using MediatR;

namespace UserService.Api.Queries
{
    public record GetAllUsersQuery : IRequest<List<UserResponse>>;
}
