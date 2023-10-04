namespace Common.Exceptions
{
    public class UserForbiddenException : Exception
    {
        public UserForbiddenException()
        {
            
        }
        public UserForbiddenException(string message):base(message)
        {
            
        }
    }
}
