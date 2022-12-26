using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDL.Exceptions
{
    public class ReservationRepositoryADOException : Exception
    {
        public ReservationRepositoryADOException(string? message) : base(message)
        {
        }

        public ReservationRepositoryADOException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
