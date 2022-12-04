using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Exceptions
{
    public class ContactDetailsException : Exception
    {
        public ContactDetailsException(string? message) : base(message)
        {
        }

        public ContactDetailsException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
