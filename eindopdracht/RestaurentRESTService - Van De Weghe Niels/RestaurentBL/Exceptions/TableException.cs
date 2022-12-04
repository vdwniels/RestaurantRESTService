using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Exceptions
{
    public class TableException : Exception
    {
        public TableException(string? message) : base(message)
        {
        }

        public TableException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
