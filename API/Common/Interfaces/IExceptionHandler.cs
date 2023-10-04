using Common.Utilities;

namespace Common.Interfaces
{
    public interface IExceptionHandler
    {
        Task<ExecutionResult<T>> HandleException<T>(Func<Task<T>> action) where T : notnull;
    }
}
