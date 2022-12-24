using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Exceptions
{
    public class TableRepositoryADOException : Exception
    {
        public TableRepositoryADOException(string? message) : base(message)
        {
        }

        public TableRepositoryADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
