using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test6.Exceptions
{
    public class SampleException : Exception
    {
        public SampleException(string message) : base(message)
        {
        }

        public SampleException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
