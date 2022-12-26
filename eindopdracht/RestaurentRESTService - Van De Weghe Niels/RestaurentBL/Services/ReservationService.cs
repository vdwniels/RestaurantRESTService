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
        private IReservationRepository repo;
        private TableService tableService;

        public ReservationService(IReservationRepository reservationRepo, TableService ts)
        {
            this.repo = reservationRepo;
            this.tableService = ts;
        }

        public Reservation AddReservation(Reservation reservation)
        {
            try
            {
                if (reservation == null) throw new ReservationServiceException("ReservationService - AddReservation - no reservation data entry");
                int tableNumber = SelectFreeTable(null,reservation.RestaurantInfo.RestaurantId, reservation.DateAndHour, reservation.Seats);
                reservation.SetTableNumber(tableNumber);
                Reservation reservationWithIdAndTableNumber = repo.AddReservation(reservation);
                return reservationWithIdAndTableNumber;
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

        public int SelectFreeTable (int? reservationId, int restaurantId, DateTime DateAndHour, int seats)
        {
            try
            {
                //List<Table> allTables = tableService.GetAllTablesOfRestaurant(restaurantId); 
                //List<Table> reservedTables = repo.SelectReservedTables(reservationId, restaurantId, DateAndHour); 

                //List<Table> FreeTables = allTables.Except(reservedTables).ToList();

                List<Table> FreeTables = repo.SelectFreeTables(restaurantId, DateAndHour);

                int freeTableWithMostSeats = FreeTables.Max(r => r.Seats);

                for (int i = seats; i <= freeTableWithMostSeats; i++)
                {
                    foreach (Table t in FreeTables)
                    {
                        if (t.Seats == i)
                        {
                            return t.TableNumber;
                        }
                    }
                }
                throw new ReservationServiceException("ReservationService - SelectFreeTable - sadly, this restaurant doesn't have any more free tables at this time");
                
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

        public Reservation UpdateReservation(Reservation reservation)
        {
            try
            {
                if (reservation == null) throw new ReservationServiceException("ReservationService - UpdateReservation - no reservation data entry");
                Reservation currentReservation = repo.GetReservation(reservation.ReservationNumber);
                if (reservation == currentReservation) throw new ReservationServiceException("ReservationService - UpdateReservation - No different values");
                int tableNumber = SelectFreeTable(reservation.ReservationNumber, reservation.RestaurantInfo.RestaurantId, reservation.DateAndHour, reservation.Seats);// when a reservation is updated, it may also need a new tablenumber that the code picks out for them, but we need to take reservationId into account because if the new reservation gets the same tablenumber, the old reservation may overlap because that is the one that gets updated 
                reservation.SetTableNumber(tableNumber);
                repo.UpdateReservation(reservation);
                return reservation;
            }
            catch (ReservationServiceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new ReservationServiceException("UpdateReservation", ex);
            }
        }

        public void CancelReservation(int reservationId)// TODO controle als niet al verstreken is
        {
            try
            {
                if (!repo.reservationExists(reservationId)) throw new ReservationServiceException("ReservationService - Cancelreservation - reservation does not exist");
                repo.CancelReservation(reservationId);
            }
            catch (ReservationServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ReservationServiceException("CancelReservation", ex);
            }
        }

        public List<Reservation> SearchReservations (int customerId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                if (!startDate.HasValue || !endDate.HasValue) throw new ReservationServiceException("ReservationService - SearchReservations - no data");
                List<Reservation> reservations = repo.SearchReservations(customerId, startDate, endDate);
                return reservations;
            }
            catch (ReservationServiceException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new ReservationServiceException("SearchReservations", ex);
            }
        }

        public List<Reservation> GetReservations (int restaurantId, DateTime? startDate, DateTime? endDate)
        {
            try
            {
                if (!startDate.HasValue || !endDate.HasValue) throw new ReservationServiceException("ReservationService - GetReservations - no data");
                List<Reservation> reservations = repo.GetReservations(restaurantId, startDate, endDate);
                return reservations;
            }
            catch (ReservationServiceException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ReservationServiceException("SearchReservations", ex);
            }

        }
    }
}
