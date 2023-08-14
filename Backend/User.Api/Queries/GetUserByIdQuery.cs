using LanguageExt.Common;
using MediatR;
using UserService.Api.Models;

namespace UserService.Api.Queries
{
    public class GetUserByIdQuery : IRequest<Result<UserResponse>>
    {
        public readonly int Id;
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }
    }
}
