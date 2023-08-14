using FluentValidation;
using LanguageExt.Common;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace UserService.Api.Validations
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, Result<TResponse>> where TRequest : notnull
    {
        private readonly IValidator<TRequest> _validators;

        public ValidationBehavior(IValidator<TRequest> validators)
        {
            _validators = validators;
        }

        public async Task<Result<TResponse>> Handle(TRequest request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
        {
            var validationResult = await _validators.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return new Result<TResponse>(new ValidationException(validationResult.Errors));
            }

            return await next();
        }
    }

}
