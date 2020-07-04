using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium2.Exceptions
{
    public class NeedSpecialEquipmentException:Exception
    {
        public NeedSpecialEquipmentException(string message) : base(message)
        {
        }

        public NeedSpecialEquipmentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
