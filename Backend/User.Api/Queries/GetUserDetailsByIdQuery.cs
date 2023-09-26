
using MediatR;
using UserService.Api.Models;

namespace UserService.Api.Queries
{
    public class GetUserDetailsByIdQuery : IRequest<UserDetailsResponse>
    {
        public readonly int Id;
        public GetUserDetailsByIdQuery(int id)
        {
            Id = id;
        }
    }
}
