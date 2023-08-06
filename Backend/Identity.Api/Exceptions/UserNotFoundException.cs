namespace Identity.Api.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException()
        {
            
        }
        public UserNotFoundException(string username) : base(String.Format("{0} is not a regestered user", username))
        {
            
        }
    }
}
