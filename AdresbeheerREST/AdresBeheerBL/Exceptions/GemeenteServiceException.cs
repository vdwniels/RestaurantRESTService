using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdresBeheerBL.Exceptions
{
    public class GemeenteServiceException : Exception
    {
        public GemeenteServiceException(string? message) : base(message)
        {
        }

        public GemeenteServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
