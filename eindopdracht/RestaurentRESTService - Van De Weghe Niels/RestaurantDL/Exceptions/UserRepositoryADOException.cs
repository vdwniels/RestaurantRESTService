using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Exceptions
{
    public class UserRepositoryADOException : Exception
    {
        public UserRepositoryADOException(string? message) : base(message)
        {
        }

        public UserRepositoryADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
