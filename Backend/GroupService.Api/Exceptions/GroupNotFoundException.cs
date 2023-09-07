namespace GroupService.Api.Exceptions
{
    public class GroupNotFoundException : Exception
    {
        public readonly string ErrorCode;
        public readonly string ErrorMessage;
        public GroupNotFoundException() : base()
        {

            ErrorCode = "ERR_GROUP_NOT_FOUND";
            ErrorMessage = "Group not found";
        }
    }
}
