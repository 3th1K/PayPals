using Microsoft.AspNetCore.Mvc;

namespace Common.Exceptions
{
    public class ApiException : Exception
    {
        public ObjectResult Result { get; private set; }

        public ApiException(ObjectResult errorObject)
        {
            Result = errorObject;
        }
    }
}
