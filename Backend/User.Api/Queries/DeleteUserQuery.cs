using Data.Models;
using MediatR;

namespace UserService.Api.Queries
{
    public class DeleteUserQuery:IRequest<User>
    {
        public readonly int Id;
        public DeleteUserQuery(int id)
        {
            Id = id;
        }
    }
}
