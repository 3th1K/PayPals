using Common.DTOs.UserDTOs;
using Common.Utilities;
using MediatR;

namespace UserService.Api.Queries
{
    public class GetUserByIdQuery : IRequest<ApiResult<UserResponse>>
    {
        public readonly int Id;
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
