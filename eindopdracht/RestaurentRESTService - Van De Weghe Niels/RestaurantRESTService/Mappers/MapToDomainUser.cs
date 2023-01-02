using RestaurantBL.Exceptions;
using RestaurantBL.Model;
using RestaurantBL.Services;
using RestaurantRESTServiceUser.Exceptions;
using RestaurantRESTServiceUser.Model.input;

namespace RestaurantRESTServiceUser.Mappers
{
    public class MapToDomainUser
    {
        public static User MapToUserDomain(UserRESTInputDTOUser dto)
        {
            try
            {
                Location location = new Location(dto.Location.PostalCode, dto.Location.Town);
                if(!string.IsNullOrWhiteSpace(dto.Location.Street) && dto.Location.Street != "string") location.SetStreet(dto.Location.Street);
                if (!string.IsNullOrWhiteSpace(dto.Location.Number) && dto.Location.Number != "string") location.SetNumber(dto.Location.Number);
                User user = new User(dto.Name, dto.Email, dto.PhoneNumber, location);
                return user;
            }
            catch (Exception ex)
            {
                throw new MapException("MapToUserDomain", ex);
            }
        }

        public static Reservation MapToReservationDomain(ReservationRESTInputDTOUser dto,RestaurantService restserv,UserService userserv)
        {
            try
            {
                Restaurant r = restserv.GetRestaurant(dto.RestaurantId);
                Reservation reservation = new Reservation(restserv.GetRestaurant(dto.RestaurantId), userserv.GetUser(dto.CustomerId), dto.Seats, dto.DateAndHour);
                return reservation;
            }
            catch(Exception ex)
            {
                throw new MapException("MapToReservationDomain", ex.InnerException);
            }
        }

        public static Reservation MapToReservationDomain(UpdateReservationRESTInputDTOUser dto, int reservationId, Reservation currentReservation)
        {
            try
            {
                Reservation newReservation = new Reservation(reservationId,currentReservation.Tablenumber,currentReservation.RestaurantInfo,currentReservation.Customer,dto.Seats,dto.DateAndHour);
                return newReservation;
            }
            catch (ReservationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new MapException("MapToReservationDomain", ex.InnerException);
            }
        }

    }
}
