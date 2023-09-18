using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities
{
    public class Error
    {
        public string ErrorCode { get; set; } = string.Empty;
        public int Code { get; set; } = 0;  
        public string ErrorMessage { get; set; } = string.Empty;
        public string ErrorDescription { get; set; } = string.Empty;
        public string ErrorSolution { get; set; } = string.Empty;
        public string ErrorDetails { get; set; } = string.Empty;
        public List<string> Errors { get; set; } = new List<string>();
    }
}
