using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Exceptions
{
    public class RestaurantRepositoryADOException : Exception
    {
        public RestaurantRepositoryADOException(string? message) : base(message)
        {
        }

        public RestaurantRepositoryADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
