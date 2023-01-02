using RestaurantBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Interfaces
{
    public interface IReservationRepository
    {
        Reservation AddReservation(Reservation reservation);
        void CancelReservation(int reservationId);
        Reservation GetReservation(int reservationNumber);
        List<Reservation> GetReservations(int restaurantId, DateTime? startDate, DateTime? endDate);
        bool reservationExists(int reservationId);
        List<Reservation> SearchReservations(int customerId, DateTime? startDate, DateTime? endDate);
        List<Table> SelectFreeTables(int restaurantId, DateTime dateAndHour);
        List<Table> SelectFreeTables(int reservationNumber, int restaurantId, DateTime reservationtime);
        void UpdateReservation(Reservation reservation);
    }
}
