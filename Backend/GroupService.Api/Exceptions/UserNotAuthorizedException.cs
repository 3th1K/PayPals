namespace GroupService.Api.Exceptions
{
    public class UserNotAuthorizedException : Exception
    {
        public readonly string ErrorCode;
        public readonly string ErrorMessage;
        public UserNotAuthorizedException() : base()
        {

            ErrorCode = "ERR_USER_NOT_AUTHORIZED";
            ErrorMessage = "User not authorized to access group";
            
        }
    }
}
