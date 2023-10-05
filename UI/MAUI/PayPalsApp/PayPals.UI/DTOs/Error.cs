using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayPals.UI.DTOs
{
    public class Error
    {
        public string ErrorType { get; set; } = string.Empty;
        public int ErrorCode { get; set; } = 0;
        public int StatusCode { get; set; } = 500;
        public string ErrorMessage { get; set; } = string.Empty;
        public string ErrorDescription { get; set; } = string.Empty;
        public string ErrorSolution { get; set; } = string.Empty;
        public string ErrorDetails { get; set; } = string.Empty;
        public List<string>? Errors { get; set; } = new();
        public Error? InnerErrors { get; set; } = null;
    }
}
