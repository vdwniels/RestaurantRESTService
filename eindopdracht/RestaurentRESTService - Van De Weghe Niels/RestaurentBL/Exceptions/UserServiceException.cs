using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Exceptions
{
    public class UserServiceException : Exception
    {
        public UserServiceException(string? message) : base(message)
        {
        }

        public UserServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
