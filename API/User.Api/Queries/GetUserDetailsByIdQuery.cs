using Common.DTOs.UserDTOs;
using Common.Utilities;
using MediatR;

namespace UserService.Api.Queries
{
    public class GetUserDetailsByIdQuery : IRequest<ApiResult<UserDetailsResponse>>
    {
        public readonly int Id;
        public GetUserDetailsByIdQuery(int id)
        {
            Id = id;
        }
    }
}
