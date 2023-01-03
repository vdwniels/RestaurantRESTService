using RestaurantBL.Model;
using RestaurantBL.Services;
using RestaurantRESTServiceAdmin.Exceptions;
using RestaurantRESTServiceAdmin.Model.input;
using RestaurantRESTServiceAdmin.Model.output;
using RestautantRESTServiceAdmin.Model.output;

namespace RestaurantRESTServiceUser.Mappers
{
    public class MapFromDomainAdmin
    {
        public static RestaurantRESTOutputDTOAdmin MapFromRestaurantDomain(Restaurant r)
        {
            try
            {
                RestaurantRESTOutputDTOAdmin dto = new RestaurantRESTOutputDTOAdmin(r.RestaurantId, r.Name, r.Cuisine, r.Email, r.PhoneNumber, r.Location.PostalCode, r.Location.Town, r.Location.Street, r.Location.Number);
                foreach (Table t in r._tables.Keys)
                {
                    dto.AddTable(new TableRESTOutputDTOAdmin(t.TableId, t.TableNumber, t.Seats,t.RestaurantId));
                }
                return dto;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromRestaurantDomain", ex);
            }
        }

        public static RestaurantRESTOutputDTOAdmin MapFromRestaurantWithoutTablesDomain(Restaurant r)
        {
            try
            {
                RestaurantRESTOutputDTOAdmin dto = new RestaurantRESTOutputDTOAdmin(r.RestaurantId, r.Name, r.Cuisine, r.Email, r.PhoneNumber, r.Location.PostalCode, r.Location.Town, r.Location.Street, r.Location.Number);
                return dto;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromRestaurantWithoutTablesDomain", ex);
            }
        }

        public static TableRESTOutputDTOAdmin MapFromTableDomain(Table t)
        {
            try
            {
                TableRESTOutputDTOAdmin dto = new TableRESTOutputDTOAdmin(t.TableId, t.TableNumber, t.Seats,t.RestaurantId);
                return dto;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromTableDomain", ex);
            }
        }
        public static List<ReservationRESTOutputDTOAdmin> MapFromReservationDomain(List<Reservation> reservations)
        {
            try
            {
                List<ReservationRESTOutputDTOAdmin> reservationOutputList = new List<ReservationRESTOutputDTOAdmin>();
                foreach (Reservation r in reservations)
                {
                    ReservationRESTOutputDTOAdmin reserv = new ReservationRESTOutputDTOAdmin(r.ReservationNumber, r.Tablenumber, r.RestaurantInfo.RestaurantId, r.Customer.CustomerNumber, r.Seats, r.DateAndHour);
                    reservationOutputList.Add(reserv);
                }
                return reservationOutputList;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromReservationDomain", ex);
            }
        }

    }
}
