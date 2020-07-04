using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Exceptions
{
    public class FireTruckIsOccupiedException:Exception
    {
        public FireTruckIsOccupiedException(string message) : base(message)
        {
        }

        public FireTruckIsOccupiedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
