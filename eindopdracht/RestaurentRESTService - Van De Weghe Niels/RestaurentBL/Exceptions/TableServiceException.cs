using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Exceptions
{
    public class TableServiceException : Exception
    {
        public TableServiceException(string? message) : base(message)
        {
        }

        public TableServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
