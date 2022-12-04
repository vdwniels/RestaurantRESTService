using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Reservation
    {
        public int ReservationNumber { get; set; }
        public Restaurant RestaurantInfo { get; set; }
    }
}
