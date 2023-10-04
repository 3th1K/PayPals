namespace Common.Utilities
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class HttpStatusCodeAttribute : Attribute
    {
        public int StatusCode { get; }

        public HttpStatusCodeAttribute(int statusCode)
        {
            StatusCode = statusCode;
        }
    }
    public enum ErrorType
    {
        //general
        [HttpStatusCode(500)]
        ErrUnknown = 100,
        [HttpStatusCode(400)]
        ErrValidationFailed = 101,

        //user
        [HttpStatusCode(401)]
        ErrUserNotAuthorized = 10001,
        [HttpStatusCode(404)]
        ErrUserNotFound = 10002,
        [HttpStatusCode(403)]
        ErrUserForbidden = 10003,
        [HttpStatusCode(409)]
        ErrUserAlreadyExists = 10004,


        //group
        [HttpStatusCode(404)]
        ErrGroupNotFound = 20001,
        [HttpStatusCode(409)]
        ErrGroupAlreadyExists = 20002,

        //expense
        [HttpStatusCode(404)]
        ErrExpenseNotFound = 30001,

        //approval
        [HttpStatusCode(409)]
        ErrApprovalAlreadyExists = 40001
    }
    public class Error
    {
        public string ErrorType { get; set; } = string.Empty;
        public int ErrorCode { get; set; } = 0;
        public int StatusCode { get; set; } = 500;
        public string ErrorMessage { get; set; } = string.Empty;
        public string ErrorDescription { get; set; } = string.Empty;
        public string ErrorSolution { get; set; } = string.Empty;
        public string ErrorDetails { get; set; } = string.Empty;
        public List<string>? Errors { get; set; } = new();

        public Error? InnerErrors { get; set; } = null;

        public static int GetHttpStatusCode(ErrorType error)
        {
            var fieldInfo = error.GetType().GetField(error.ToString());
            var attribute = fieldInfo.GetCustomAttributes(typeof(HttpStatusCodeAttribute), false).FirstOrDefault() as HttpStatusCodeAttribute;

            if (attribute != null)
            {
                return attribute.StatusCode;
            }

            return 500;
        }
    }
}
