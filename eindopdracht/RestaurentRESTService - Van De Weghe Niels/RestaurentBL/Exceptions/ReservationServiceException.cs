using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Exceptions
{
    public class ReservationServiceException : Exception
    {
        public ReservationServiceException(string? message) : base(message)
        {
        }

        public ReservationServiceException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
