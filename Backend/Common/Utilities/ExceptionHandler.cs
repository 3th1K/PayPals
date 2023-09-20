using Common.Exceptions;
using Common.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public class ExceptionHandler : IExceptionHandler
    {
        private IErrorBuilder _errorBuilder;
        public ExceptionHandler(IErrorBuilder errorBuilder)
        {

            _errorBuilder = errorBuilder;

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
                return new ObjectResult(error) { StatusCode = 403 };
            }
            catch (UserNotFoundException ex)
            {
                var error = _errorBuilder.BuildError(ex, ex.Message);
                return new ObjectResult(error) { StatusCode = 401 };
            }
            catch (GroupAlreadyExistsException ex)
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
