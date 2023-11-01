using System.Reflection;

namespace PayPals.UI.DTOs.ExpenseDTOs
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)] 
    sealed class StatusMessageAttribute : Attribute
    {
        public string Message { get; }

        public StatusMessageAttribute(string message)
        {
            Message = message;
        }
    }

    public enum ExpenseStatus
    {
        [StatusMessage("Approved")]
        Approved,
        [StatusMessage("Rejected")]
        Rejected,
        [StatusMessage("Active")]
        Active
    }

    public class ExpenseStatusResponse
    {
        public int ExpenseId { get; set; }

        public int TotalMembers { get; set; }
        public int? ApprovalReceived { get; set; }
        public List<ExpenseApprovalResponse> ExpenseApprovals { get; set; } = new List<ExpenseApprovalResponse>();
        public ExpenseStatus Status { get; set; }
        public string StatusMessage
        {
            get
            {
                var enumType = typeof(ExpenseStatus);
                var memberInfo = enumType.GetMember(Status.ToString()).FirstOrDefault();

                if (memberInfo != null)
                {
                    var attribute = memberInfo.GetCustomAttribute<StatusMessageAttribute>();
                    if (attribute != null)
                    {
                        return attribute.Message;
                    }
                }
                return "Unknown";
            }
        }
    }
}
