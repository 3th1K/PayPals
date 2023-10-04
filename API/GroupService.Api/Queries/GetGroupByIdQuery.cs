using Common.DTOs.GroupDTOs;
using Common.Utilities;
using MediatR;

namespace GroupService.Api.Queries
{
    public class GetGroupByIdQuery : IRequest<ApiResult<GroupResponse>>
    {
        public readonly int Id;
        public GetGroupByIdQuery(int id)
        {

            Id = id;

        }
    }
}
