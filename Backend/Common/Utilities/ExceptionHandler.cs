using Common.Exceptions;
using Common.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace Common.Utilities
{
    public class ExceptionHandler : IExceptionHandler
    {
        private readonly IErrorBuilder _errorBuilder;
        public ExceptionHandler()
        {

            _errorBuilder = new ErrorBuilder();

        }

        public async Task<IActionResult> HandleException<TException>(Func<Task<IActionResult>> action) where TException : Exception
        {
            try
            {
                return await action();
            }
            catch (ValidationException ex)
            {
                var validationErrors = ex.Errors.Select(error => error.ErrorMessage).ToList();
                var error = _errorBuilder.BuildError(ex, ex.Message, validationErrors);
                return new ObjectResult(error) { StatusCode = 400 };
            }

            //User exceptions
            catch (UserNotFoundException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 404 };
            }
            catch (UserNotAuthorizedException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 401 };
            }
            catch (UserForbiddenException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 403 };
            }
            catch (UserAlreadyExistsException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 409 };
            }

            //Group exceptions
            catch (GroupNotFoundException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 404 };
            }
            catch (GroupAlreadyExistsException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 409 };
            }

            //expense exceptions
            catch (ExpenseNotFoundException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 404 };
            }

            //approval exceptions
            catch (ApprovalAlreadyExistsException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 409 };
            }
            catch (Exception ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 500 };
            }
        }
    }
}
