using Common.Utilities;
using MediatR;
using Newtonsoft.Json.Linq;

namespace Identity.Api.Commands
{
    public record LoginRequestCommand(string Username, string Password) : IRequest<ApiResult<string>>;
}
