public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException() : base(String.Format("USER_ALREADY_EXISTS"))
    {
    }
}