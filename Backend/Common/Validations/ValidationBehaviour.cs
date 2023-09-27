﻿using FluentValidation;
using MediatR;

namespace Common.Validations
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly IValidator<TRequest> _validators;

        public ValidationBehavior(IValidator<TRequest> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var validationResult = await _validators.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
                //return new Result<TResponse>(new ValidationException(validationResult.Errors));
                //return new ApiResult<TResponse>(Errors.ErrValidationException, validationResult.Errors.Select(error => error.ErrorMessage).ToList())
            }

            return await next();
        }
    }
}
