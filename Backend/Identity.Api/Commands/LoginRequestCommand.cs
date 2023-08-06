using Identity.Api.Exceptions;
using Identity.Api.Interfaces;
using Identity.Api.Models;
using Identity.Api.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Commands
{
    public record LoginRequestCommand(string Username, string Password) : IRequest<string>;

    public class LoginRequestCommandHandler : IRequestHandler<LoginRequestCommand, string>
    {
        private readonly IIdentityRepository _repository;
        public LoginRequestCommandHandler(IIdentityRepository repository)
        {
            _repository = repository;
        }
        public async Task<string> Handle(LoginRequestCommand request, CancellationToken cancellationToken)
        {
            try 
            {
                var token =  await _repository.GetToken(request.Username, request.Password);
                return token;
            }
            catch(UserNotFoundException)
            {
                // do something
                throw;
            }
            catch (UserNotAuthorizedException)
            {
                // do something
                throw;
            }
        }
    }
}
