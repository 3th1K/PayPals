using PayPals.UI.DTOs;
using PayPals.UI.Utilities;

namespace PayPals.UI.Interfaces
{
    public interface ILoginService
    {
        Task<ApiResult<string>> DoLogin(LoginRequest request);
    }
}
