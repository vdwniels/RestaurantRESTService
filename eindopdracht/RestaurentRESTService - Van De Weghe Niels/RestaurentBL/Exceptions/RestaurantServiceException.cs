using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Exceptions
{
    public class RestaurantServiceException : Exception
    {
        public RestaurantServiceException(string? message) : base(message)
        {
        }

        public RestaurantServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
