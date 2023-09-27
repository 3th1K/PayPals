using Data.DTOs.UserDTOs;
using MediatR;

namespace UserService.Api.Queries
{
    public class GetUserByIdQuery : IRequest<UserResponse>
    {
        public readonly int Id;
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
