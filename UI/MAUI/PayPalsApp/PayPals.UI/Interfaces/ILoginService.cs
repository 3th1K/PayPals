using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PayPals.UI.DTOs;
using PayPals.UI.Utilities;

namespace PayPals.UI.Interfaces
{
    public interface ILoginService
    {
        Task<ApiResult<string>> DoLogin(LoginRequest request);
    }
}
