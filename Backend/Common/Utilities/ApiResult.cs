using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Common.Utilities
{
    public class ApiResult<T>
    {
        private IErrorBuilder _errorBuilder;
        public ObjectResult Value { get; private set; }

        public ApiResult(T successObject)
        {
            this.Value = new ObjectResult(successObject) { StatusCode = 200 };
        }
        public ApiResult(Error errorObject)
        {
            this.Value = new ObjectResult(errorObject) { StatusCode = errorObject.StatusCode };
        }

        public ApiResult<T> Success(T successObject)
        {
            return new ApiResult<T>(successObject);
        }

        public ApiResult<T> Failure(ErrorType error, string details = "No Details Available", List<string>? errors = null)
        {
            _errorBuilder = new ErrorBuilder();
            var errorObject = _errorBuilder.BuildError(error, details, errors);
            return new ApiResult<T>(errorObject);
        }
    }
}
