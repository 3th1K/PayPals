using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class ApprovalAlreadyExistsException : Exception
    {
        public ApprovalAlreadyExistsException()
        {
            
        }

        public ApprovalAlreadyExistsException(string message):base(message)
        {
            
        }
    }
}
