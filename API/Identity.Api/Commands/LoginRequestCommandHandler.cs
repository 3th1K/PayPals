using Common.Exceptions;
using Common.Utilities;
using Identity.Api.Interfaces;
using MediatR;

namespace Identity.Api.Commands;

public class LoginRequestCommandHandler : IRequestHandler<LoginRequestCommand, ApiResult<string>>
{
    private readonly IIdentityRepository _repository;
    private readonly ILogger<LoginRequestCommandHandler> _logger;
    public LoginRequestCommandHandler(IIdentityRepository repository, ILogger<LoginRequestCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<ApiResult<string>> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var token = await _repository.GetToken(request.Username, request.Password);
            return ApiResult<string>.Success(token);
        }
        catch (UserNotFoundException ex)
        {
            _logger.LogError("Cannon Find User In Database");
            return ApiResult<string>.Failure(ErrorType.ErrUserNotFound, "Invalid user");
        }
        catch (UserNotAuthorizedException ex)
        {
            _logger.LogError("Provided Password Is Incorrect");
            return ApiResult<string>.Failure(ErrorType.ErrUserNotAuthorized, "Incorrect password");
        }
        catch (Exception ex) {
            _logger.LogError($"Exception Encountered {ex.Message}");
            return ApiResult<string>.Failure(ex);
        }
    }
}