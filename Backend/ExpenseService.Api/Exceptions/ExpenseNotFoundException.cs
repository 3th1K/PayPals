namespace ExpenseService.Api.Exceptions
{
    public class ExpenseNotFoundException : Exception
    {
        public readonly string ErrorCode;
        public readonly string ErrorMessage;
        public ExpenseNotFoundException() : base()
        {
            ErrorCode = "EXPENSE_NOT_FOUND";
            ErrorMessage = "Expense not found";
        }
    }
}
