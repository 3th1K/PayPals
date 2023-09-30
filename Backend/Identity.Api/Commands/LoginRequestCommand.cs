using Common.Utilities;
using MediatR;

namespace Identity.Api.Commands
{
    public record LoginRequestCommand(string Username, string Password) : IRequest<ApiResult<string>>;
}
