using Common.Models;
using Common.Utilities;
using MediatR;

namespace UserService.Api.Queries
{
    public class DeleteUserQuery:IRequest<ApiResult<User>>
    {
        public readonly int Id;
        public DeleteUserQuery(int id)
        {
            Id = id;
        }
    }
}
