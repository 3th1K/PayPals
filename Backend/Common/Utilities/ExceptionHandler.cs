using Common.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Common.Utilities
{
    public class ExecutionResult<T>
    {
        public bool Success { get; set; }
        public ObjectResult? ErrorObject { get; set; }
        public T Result { get; set; }
    }

    public class ExceptionHandler : IExceptionHandler
    {
        private readonly IErrorBuilder _errorBuilder;

        //public bool Success { get; private set; }
        //public Error ErrorObject { get; private set; }

        public ExceptionHandler()
        {

            _errorBuilder = new ErrorBuilder();

        }

        public async Task<ExecutionResult<T>> HandleException<T>(Func<Task<T>> action) where T : notnull
        {
            var result = new ExecutionResult<T>();
            try
            {
                result.Result = await action();
                result.Success = true;
            }
            catch (ValidationException ex)
            {
                var validationErrors = ex.Errors.Select(error => error.ErrorMessage).ToList();
                var error = _errorBuilder.BuildError(ex, ex.Message, validationErrors);
                result.ErrorObject = new ObjectResult(error) { StatusCode = error.StatusCode };
                result.Success = false;
            }
            catch (Exception ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                result.ErrorObject = new ObjectResult(error) { StatusCode = error.StatusCode };
                result.Success = false;
            }
            return result;

            ////User exceptions
            //catch (UserNotFoundException ex)
            //{
            //    var error = _errorBuilder.BuildError(ex, ex.Message);
            //    return new ObjectResult(error) { StatusCode = 404 };
            //}
            //catch (UserNotAuthorizedException ex)
            //{
            //    var error = _errorBuilder.BuildError(ex, ex.Message);
            //    return new ObjectResult(error) { StatusCode = 401 };
            //}
            //catch (UserForbiddenException ex)
            //{
            //    var error = _errorBuilder.BuildError(ex, ex.Message);
            //    return new ObjectResult(error) { StatusCode = 403 };
            //}
            //catch (UserAlreadyExistsException ex)
            //{
            //    var error = _errorBuilder.BuildError(ex, ex.Message);
            //    return new ObjectResult(error) { StatusCode = 409 };
            //}

            ////Group exceptions
            //catch (GroupNotFoundException ex)
            //{
            //    var error = _errorBuilder.BuildError(ex, ex.Message);
            //    return new ObjectResult(error) { StatusCode = 404 };
            //}
            //catch (GroupAlreadyExistsException ex)
            //{
            //    var error = _errorBuilder.BuildError(ex, ex.Message);
            //    return new ObjectResult(error) { StatusCode = 409 };
            //}

            ////expense exceptions
            //catch (ExpenseNotFoundException ex)
            //{
            //    var error = _errorBuilder.BuildError(ex, ex.Message);
            //    return new ObjectResult(error) { StatusCode = 404 };
            //}

            ////approval exceptions
            //catch (ApprovalAlreadyExistsException ex)
            //{
            //    var error = _errorBuilder.BuildError(ex, ex.Message);
            //    return new ObjectResult(error) { StatusCode = 409 };
            //}
            //catch (Exception ex)
            //{
            //    var error = _errorBuilder.BuildError(ex, ex.Message);
            //    return new ObjectResult(error) { StatusCode = 500 };
            //}
        }





    }
}
