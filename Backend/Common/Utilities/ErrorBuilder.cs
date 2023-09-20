using Common.Exceptions;
using Common.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Common.Utilities
{
    public enum Errors
    {
        //general
        ERR_UNKNOWN = 100,
        ERR_VALIDATION_FAILED = 101,

        //user
        ERR_USER_NOT_AUTHORIZED = 10001,
        ERR_USER_NOT_FOUND = 10002,
        ERR_USER_FORBIDDEN = 10003,
        ERR_USER_ALREADY_EXISTS = 10004,
        

        //group
        ERR_GROUP_NOT_FOUND = 20001,
        ERR_GROUP_ALREADY_EXISTS = 20002,

        //expense
        ERR_EXPENSE_NOT_FOUND = 30001,
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
                //general
                case "ValidationException":
                    return Errors.ERR_VALIDATION_FAILED;

                //user
                case "UserNotFoundException":
                    return Errors.ERR_USER_NOT_FOUND;
                case "UserNotAuthorizedException":
                    return Errors.ERR_USER_NOT_AUTHORIZED;
                case "UserForbiddenException":
                    return Errors.ERR_USER_FORBIDDEN;
                case "UserAlreadyExistsException":
                    return Errors.ERR_USER_ALREADY_EXISTS;

                //expense
                case "ExpenseNotFoundException":
                    return Errors.ERR_EXPENSE_NOT_FOUND;

                //group
                case "GroupNotFoundException":
                    return Errors.ERR_GROUP_NOT_FOUND;
                case "GroupAlreadyExistsException":
                    return Errors.ERR_GROUP_ALREADY_EXISTS;

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
