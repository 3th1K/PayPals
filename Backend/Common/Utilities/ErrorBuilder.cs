using Common.Exceptions;
using Common.Interfaces;
using FluentValidation;

namespace Common.Utilities
{
    public enum Errors
    {
        ERR_USER_NOT_FOUND,
        ERR_UNKNOWN,
        ERR_VALIDATION_FAILED,
        ERR_USER_NOT_AUTHORIZED
    }
    public class ErrorBuilder : IErrorBuilder
    {
        private IResourceAccessor _accessor;
        public ErrorBuilder()
        {
            _accessor = new ResourceAccessor();
        }

        private Errors GetErrorByException(Exception exception) {

            switch (exception.GetType().Name)
            {
                case "UserNotFoundException":
                    return Errors.ERR_USER_NOT_FOUND;
                case "ValidationException":
                    return Errors.ERR_VALIDATION_FAILED;
                case "UserNotAuthorizedException":
                    return Errors.ERR_USER_NOT_AUTHORIZED;
                default:
                    return Errors.ERR_UNKNOWN;
            }
        }
        public Error BuildError(Exception exception, List<string>? errors = null)
        {
            var error = GetErrorByException(exception);
            var resourceInfo = _accessor.GetResourceInfo(error);
            
            return new Error
            {
                ErrorCode = resourceInfo.Name,
                Code = resourceInfo.ValueCode,
                ErrorMessage = resourceInfo.ValueMessage,
                ErrorDescription = resourceInfo.ValueDescription,
                ErrorSolution = resourceInfo.ValueSolution,
                Errors = errors ?? new List<string>() { }
            };
            
        }

        public Error BuildError(Exception exception, string details, List<string>? errors = null)
        {
            var error = GetErrorByException(exception);
            var resourceInfo = _accessor.GetResourceInfo(error);

            return new Error
            {
                ErrorCode = resourceInfo.Name,
                Code = resourceInfo.ValueCode,
                ErrorMessage = resourceInfo.ValueMessage,
                ErrorDescription = resourceInfo.ValueDescription,
                ErrorSolution = resourceInfo.ValueSolution,
                ErrorDetails = details,
                Errors = errors ?? new List<string>() { }
            };
        }

        public Error BuildError(Errors error, List<string>? errors = null)
        {
            var resourceInfo = _accessor.GetResourceInfo(error);

            return new Error
            {
                ErrorCode = resourceInfo.Name,
                Code = resourceInfo.ValueCode,
                ErrorMessage = resourceInfo.ValueMessage,
                ErrorDescription = resourceInfo.ValueDescription,
                ErrorSolution = resourceInfo.ValueSolution,
                Errors = errors ?? new List<string>() { }
            };
        }

        public Error BuildError(Errors error, string details, List<string>? errors = null)
        {
            var resourceInfo = _accessor.GetResourceInfo(error);

            return new Error
            {
                ErrorCode = resourceInfo.Name,
                Code = resourceInfo.ValueCode,
                ErrorMessage = resourceInfo.ValueMessage,
                ErrorDescription = resourceInfo.ValueDescription,
                ErrorSolution = resourceInfo.ValueSolution,
                ErrorDetails = details,
                Errors = errors ?? new List<string>() { }
            };
        }
    }
}
