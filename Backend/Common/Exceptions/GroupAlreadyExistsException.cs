using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class GroupAlreadyExistsException : Exception
    {
        public GroupAlreadyExistsException()
        {
            
        }
        public GroupAlreadyExistsException(string message) : base(message)
        {
            
        }
    }
}
