using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Exceptions
{
    public class ActionNotFoundException:Exception
    {
        public ActionNotFoundException(string message) : base(message)
        {
        }

        public ActionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
