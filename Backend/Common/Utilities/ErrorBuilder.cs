using Common.Interfaces;

namespace Common.Utilities
{
    public class ErrorBuilder : IErrorBuilder
    {
        private readonly IResourceAccessor _accessor;
        public ErrorBuilder()
        {
            _accessor = new ResourceAccessor();
        }

        private Error? GetInnerExceptions(Exception? ex)
        {
            Error? error = null;

            if (ex != null)
            {
                string? errDetails = null;
                foreach (var er in ex.Data.Keys)
                {
                    errDetails += $"[ {er} : {ex.Data[er]} ] ";
                }
                error = new Error()
                {
                    ErrorType = ex.GetType().Name,
                    ErrorCode = ex.HResult,
                    StatusCode = 500,
                    ErrorMessage = ex.Message,
                    ErrorDescription = ex.StackTrace ?? "No Description Available",
                    ErrorSolution = ex.HelpLink ?? "Please Debug Through Code",
                    ErrorDetails = errDetails ?? "No Details Available",
                    Errors = null,
                    InnerErrors = GetInnerExceptions(ex.InnerException)
                };
            }
            return error;
        }
        private ErrorType GetErrorByException(Exception exception) {

            switch (exception.GetType().Name)
            {
                //general
                case "ValidationException":
                    return ErrorType.ErrValidationFailed;

                //user
                case "UserNotFoundException":
                    return ErrorType.ErrUserNotFound;
                case "UserNotAuthorizedException":
                    return ErrorType.ErrUserNotAuthorized;
                case "UserForbiddenException":
                    return ErrorType.ErrUserForbidden;
                case "UserAlreadyExistsException":
                    return ErrorType.ErrUserAlreadyExists;

                //expense
                case "ExpenseNotFoundException":
                    return ErrorType.ErrExpenseNotFound;

                //group
                case "GroupNotFoundException":
                    return ErrorType.ErrGroupNotFound;
                case "GroupAlreadyExistsException":
                    return ErrorType.ErrGroupAlreadyExists;

                //approval
                case "ApprovalAlreadyExistsException":
                    return ErrorType.ErrApprovalAlreadyExists;

                default:
                    return ErrorType.ErrUnknown;
            }
        }
        public Error BuildError(Exception exception, List<string>? errors = null)
        {
            var error = GetErrorByException(exception);
            var resourceInfo = _accessor.GetResourceInfo(error);
            
            return new Error
            {
                ErrorType = resourceInfo.Name,
                ErrorCode = resourceInfo.ValueCode,
                StatusCode = 500,
                ErrorMessage = resourceInfo.ValueMessage,
                ErrorDescription = resourceInfo.ValueDescription,
                ErrorSolution = resourceInfo.ValueSolution,
                Errors = errors,
                InnerErrors = GetInnerExceptions(exception.InnerException)
            };
            
        }

        public Error BuildError(Exception exception, string details, List<string>? errors = null)
        {
            var error = GetErrorByException(exception);
            var resourceInfo = _accessor.GetResourceInfo(error);

            return new Error
            {
                ErrorType = resourceInfo.Name,
                ErrorCode = resourceInfo.ValueCode,
                StatusCode = 500,
                ErrorMessage = resourceInfo.ValueMessage,
                ErrorDescription = resourceInfo.ValueDescription,
                ErrorSolution = resourceInfo.ValueSolution,
                ErrorDetails = details,
                Errors = errors,
                InnerErrors = GetInnerExceptions(exception.InnerException)
            };
        }

        public Error BuildError(ErrorType error, List<string>? errors = null)
        {
            var resourceInfo = _accessor.GetResourceInfo(error);
            var statusCode = Error.GetHttpStatusCode(error);
            return new Error
            {
                ErrorType = resourceInfo.Name,
                ErrorCode = resourceInfo.ValueCode,
                StatusCode = statusCode,
                ErrorMessage = resourceInfo.ValueMessage,
                ErrorDescription = resourceInfo.ValueDescription,
                ErrorSolution = resourceInfo.ValueSolution,
                Errors = errors
            };
        }

        public Error BuildError(ErrorType error, string details, List<string>? errors = null)
        {
            var resourceInfo = _accessor.GetResourceInfo(error);
            var statusCode = Error.GetHttpStatusCode(error);
            return new Error
            {
                ErrorType = resourceInfo.Name,
                ErrorCode = resourceInfo.ValueCode,
                StatusCode = statusCode,
                ErrorMessage = resourceInfo.ValueMessage,
                ErrorDescription = resourceInfo.ValueDescription,
                ErrorSolution = resourceInfo.ValueSolution,
                ErrorDetails = details,
                Errors = errors
            };
        }

        
    }
}
