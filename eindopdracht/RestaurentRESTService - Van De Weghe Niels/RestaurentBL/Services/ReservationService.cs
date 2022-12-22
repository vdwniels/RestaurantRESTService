using RestaurantBL.Exceptions;
using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Services
{
    public class ReservationService
    {
        private IReservationRepository reservationRepo;
        private TableService tableService;

        public ReservationService(IReservationRepository reservationRepo, TableService ts)
        {
            this.reservationRepo = reservationRepo;
            this.tableService = ts;
        }

        public Reservation AddReservation(Reservation reservation)
        {
            try
            {
                if (reservation == null) throw new ReservationServiceException("ReservationService - AddReservation - no reservation data entry");
                
            }
            catch (ReservationServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ReservationServiceException("AddReservation", ex);
            }
        }

        private Table SelectFreeTable (int restaurantId, DateTime DateAndHour, int seats)
        {
            try
            {
                List<Table> allTables = tableService.GetAllTablesOfRestaurant(restaurantId); 
                List<Table> reservedTables = reservationRepo.SelectFreeTables(restaurantId, DateAndHour);

                List<Table> FreeTables = allTables.Except(reservedTables).ToList();
            }
            catch (ReservationServiceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new ReservationServiceException("SelectFreeTable", ex);
            }
        }
    }
}
