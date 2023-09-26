using Microsoft.AspNetCore.Mvc;

namespace Common.Interfaces
{
    public interface IExceptionHandler
    {
        Task<IActionResult> HandleException<TException>(Func<Task<IActionResult>> action) where TException : Exception;
    }
}
