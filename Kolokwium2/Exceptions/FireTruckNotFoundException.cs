using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Exceptions
{
    public class FireTruckNotFoundException:Exception
    {
        public FireTruckNotFoundException(string message) : base(message)
        {
        }

        public FireTruckNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
