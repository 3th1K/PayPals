using AutoMapper.Configuration.Conventions;
using GroupService.Api.Models;
using MediatR;

namespace GroupService.Api.Queries
{
    public class DeleteGroupQuery : IRequest<GroupResponse>
    {
        public readonly int GroupId;
        public DeleteGroupQuery(int id)
        {
            GroupId = id;
        }
    }
}
