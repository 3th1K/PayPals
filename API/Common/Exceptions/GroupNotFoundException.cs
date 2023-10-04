namespace Common.Exceptions
{
    public class GroupNotFoundException : Exception
    {
        public GroupNotFoundException()
        {
            
        }
        public GroupNotFoundException(string message) : base(message) { }
    }
}
