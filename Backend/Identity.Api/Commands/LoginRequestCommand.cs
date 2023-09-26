using Common.Exceptions;
using Identity.Api.Interfaces;
using MediatR;

namespace Identity.Api.Commands
{
    public record LoginRequestCommand(string Username, string Password) : IRequest<string>;

    public class LoginRequestCommandHandler : IRequestHandler<LoginRequestCommand, string>
    {
        private readonly IIdentityRepository _repository;
        private readonly ILogger<LoginRequestCommandHandler> _logger;
        public LoginRequestCommandHandler(IIdentityRepository repository, ILogger<LoginRequestCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<string> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var token = await _repository.GetToken(request.Username, request.Password);
                return token;
            }
            catch (UserNotFoundException ex)
            {
                _logger.LogError("Cannon Find User In Database");
                throw;
            }
            catch (UserNotAuthorizedException ex)
            {
                _logger.LogError("Provided Password Is Incorrect");
                throw;
            }
            catch (Exception ex) {
                _logger.LogError($"Exception Encountered {ex.Message}");
                throw;
            }
        }
    }
}
