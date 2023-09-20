using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    public interface IExceptionHandler
    {
        Task<IActionResult> HandleException<TException>(Func<Task<IActionResult>> action) where TException : Exception;
    }
}
