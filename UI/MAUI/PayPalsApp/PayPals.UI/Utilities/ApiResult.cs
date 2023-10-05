using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayPals.UI.DTOs;

namespace PayPals.UI.Utilities
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

        //private static readonly ErrorBuilder _errorBuilder = new ErrorBuilder();

        public T SuccessResult { get; private set; }
        public Error ErrorResult { get; private set; }
        public Type ResultType { get; private set; }
        public ApiResultStatus ResultStatus { get; private set; }

        public ApiResult()
        {
        }

        private ApiResult(Type resultDataType, object result, ApiResultStatus resultStatus)
        {
            ResultStatus = resultStatus;
            ResultType = resultDataType;

            if(resultStatus == ApiResultStatus.Success)
                SuccessResult = (T)result;
            else
                ErrorResult = (Error)result;
        }

        public static ApiResult<T> Success(T successObject)
        {
            return new ApiResult<T>(typeof(T), successObject, ApiResultStatus.Success);
        }

        public static ApiResult<T> Failure(Error errorObject)
        {
            //var errorObject = _errorBuilder.BuildError(error, details, errors);
            //var result = new ObjectResult(errorObject) { StatusCode = errorObject.StatusCode };
            return new ApiResult<T>(errorObject.GetType(), errorObject, ApiResultStatus.Error);

        }

    }
}
