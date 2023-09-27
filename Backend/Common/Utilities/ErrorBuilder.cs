using Common.Interfaces;

namespace Common.Utilities
{
    public enum Errors
    {
        //general
        ErrUnknown = 100,
        ErrValidationFailed = 101,

        //user
        ErrUserNotAuthorized = 10001,
        ErrUserNotFound = 10002,
        ErrUserForbidden = 10003,
        ErrUserAlreadyExists = 10004,
        

        //group
        ErrGroupNotFound = 20001,
        ErrGroupAlreadyExists = 20002,

        //expense
        ErrExpenseNotFound = 30001,

        //approval
        ErrApprovalAlreadyExists = 40001
    }
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
                    ErrorCode = ex.GetType().Name,
                    Code = ex.HResult,
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
        private Errors GetErrorByException(Exception exception) {

            switch (exception.GetType().Name)
            {
                //general
                case "ValidationException":
                    return Errors.ErrValidationFailed;

                //user
                case "UserNotFoundException":
                    return Errors.ErrUserNotFound;
                case "UserNotAuthorizedException":
                    return Errors.ErrUserNotAuthorized;
                case "UserForbiddenException":
                    return Errors.ErrUserForbidden;
                case "UserAlreadyExistsException":
                    return Errors.ErrUserAlreadyExists;

                //expense
                case "ExpenseNotFoundException":
                    return Errors.ErrExpenseNotFound;

                //group
                case "GroupNotFoundException":
                    return Errors.ErrGroupNotFound;
                case "GroupAlreadyExistsException":
                    return Errors.ErrGroupAlreadyExists;

                //approval
                case "ApprovalAlreadyExists":
                    return Errors.ErrApprovalAlreadyExists;

                default:
                    return Errors.ErrUnknown;
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
                ErrorCode = resourceInfo.Name,
                Code = resourceInfo.ValueCode,
                ErrorMessage = resourceInfo.ValueMessage,
                ErrorDescription = resourceInfo.ValueDescription,
                ErrorSolution = resourceInfo.ValueSolution,
                ErrorDetails = details,
                Errors = errors,
                InnerErrors = GetInnerExceptions(exception.InnerException)
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
                Errors = errors
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
                Errors = errors
            };
        }

        
    }
}
