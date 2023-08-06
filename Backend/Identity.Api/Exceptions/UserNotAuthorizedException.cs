namespace Identity.Api.Exceptions
{
    public class UserNotAuthorizedException : Exception
    {
        public UserNotAuthorizedException()
        {
            
        }

        public UserNotAuthorizedException(string password) : base(String.Format("Incorrect Password : {0}", password))
        {
            
        }
    }
}
