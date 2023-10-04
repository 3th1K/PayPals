namespace Common.Exceptions
{
    public class ExpenseNotFoundException : Exception
    {
        public ExpenseNotFoundException()
        {
            
        }
        public ExpenseNotFoundException(string message) : base(message)
        {
           
        }
    }
}
