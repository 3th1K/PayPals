public class UserAlreadyExistsException : Exception
{
    public UserAlreadyExistsException() : base(String.Format("user already exists"))
    {
    }
}