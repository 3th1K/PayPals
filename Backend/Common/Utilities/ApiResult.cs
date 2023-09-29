using Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Common.Utilities
{
    public enum ApiResultStatus
    {
        Success = 0,
        Warning = 4,
        Error = 8,
        Fatal = 16,
    }
    public class ApiResult<T>
    {
        
        private static readonly ErrorBuilder _errorBuilder = new ErrorBuilder();

        public ObjectResult Result { get; private set; }
        public Type ResultType { get; private set; }
        public  ApiResultStatus ResultStatus { get; private set; }

        public ApiResult()
        {
        }

        private ApiResult(Type resultDataType, ObjectResult result, ApiResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
            ResultType = resultDataType;
            Result = result;
        }

        public static ApiResult<T> Success(T successObject)
        {
            var result = new ObjectResult(successObject) { StatusCode = 200 };
            return new ApiResult<T>(typeof(T), result, ApiResultStatus.Success);
        }

        public static ApiResult<T> Failure(ErrorType error, string details = "No Details Available", List<string>? errors = null)
        {
            var errorObject = _errorBuilder.BuildError(error, details, errors);
            var result = new ObjectResult(errorObject) { StatusCode = errorObject.StatusCode };
            return new ApiResult<T> (errorObject.GetType(), result, ApiResultStatus.Error);
           
        }

        public static ApiResult<T> Failure(Exception ex, List<string>? errors = null)
        {
            //_errorBuilder = new ErrorBuilder();
            var errorObject = _errorBuilder.BuildError(ex, ex.Message, errors);
            var result = new ObjectResult(errorObject) { StatusCode = errorObject.StatusCode };
            return new ApiResult<T>(errorObject.GetType(), result, ApiResultStatus.Error);
        }

        public static ApiResult<T> Failure(Exception ex)
        {
            //_errorBuilder = new ErrorBuilder();
            var errorObject = _errorBuilder.BuildError(ex, ex.Message);
            var result = new ObjectResult(errorObject) { StatusCode = errorObject.StatusCode };
            return new ApiResult<T>(errorObject.GetType(), result, ApiResultStatus.Error);
        }

    }
}
