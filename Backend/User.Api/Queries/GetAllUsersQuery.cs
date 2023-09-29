using Common.DTOs.UserDTOs;
using Common.Utilities;
using MediatR;

namespace UserService.Api.Queries
{
    public record GetAllUsersQuery : IRequest<ApiResult<List<UserResponse>>>;
}
