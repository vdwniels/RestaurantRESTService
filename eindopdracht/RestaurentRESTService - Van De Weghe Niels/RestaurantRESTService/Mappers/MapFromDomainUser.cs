using RestaurantBL.Model;
using RestaurantBL.Services;
using RestaurantRESTServiceUser.Exceptions;
using RestaurantRESTServiceUser.Model.input;
using RestaurantRESTServiceUser.Model.output;
using System;

namespace RestaurantRESTServiceUser.Mappers
{
    public class MapFromDomainUser
    {
        public static UserRESTOutputDTOUser MapFromUserDomain(string url, User user, UserService userService)
        {
            try
            {
                string userURL = $"{url}/user/{user.CustomerNumber}";
                UserRESTOutputDTOUser dto = new UserRESTOutputDTOUser(userURL, user.CustomerNumber, user.Name, user.Email, user.PhoneNumber, user.Location.PostalCode, user.Location.Town, user.Location.Street, user.Location.Number);
                return dto;
            }
            catch(Exception ex)
            {
                throw new MapException("MapFromUserDomain", ex);
            }
        }

        public  static ReservationRESTOutputDTOUser MapFromReservationDomain(Reservation reservation)
        {
            try
            {
                
                ReservationRESTOutputDTOUser dto = new ReservationRESTOutputDTOUser(reservation.ReservationNumber,reservation.Tablenumber,reservation.RestaurantInfo.RestaurantId,reservation.Customer.CustomerNumber,reservation.Seats,reservation.DateAndHour);
                return dto;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromReservationDomain", ex);
            }
        }

        public static List<ReservationRESTOutputDTOUser> MapFromSearchReservationDomain(List<Reservation> reservations)
        {
            try
            {
                List<ReservationRESTOutputDTOUser> reservationOutputList = new List<ReservationRESTOutputDTOUser>();
                foreach (Reservation r in reservations)
                {
                    ReservationRESTOutputDTOUser reserv = new ReservationRESTOutputDTOUser(r.ReservationNumber, r.Tablenumber, r.RestaurantInfo.RestaurantId, r.Customer.CustomerNumber, r.Seats, r.DateAndHour);
                    reservationOutputList.Add(reserv);
                }
                return reservationOutputList;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromSearchReservationDomain", ex);
            }
        }
        public static ReservationRESTOutputDTOUser MapFromPutReservationDomain(Reservation r)
        {
            try
            {

                ReservationRESTOutputDTOUser reserv = new ReservationRESTOutputDTOUser(r.ReservationNumber, r.Tablenumber, r.RestaurantInfo.RestaurantId, r.Customer.CustomerNumber, r.Seats, r.DateAndHour);
                return reserv;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromPutReservationDomain", ex);
            }
        }

        public static List<RestaurantsRESTOutputDTOUser> MapFromSearchRestaurantFreeTableDomain(IReadOnlyList<Restaurant> restaurants)
        {
            try
            {
                List<RestaurantsRESTOutputDTOUser> restaurantsOutputList = new List<RestaurantsRESTOutputDTOUser>();
                foreach (Restaurant r in restaurants)
                {
                    RestaurantsRESTOutputDTOUser restaurant = new RestaurantsRESTOutputDTOUser(r.RestaurantId,r.Name,r.Cuisine,r.Email,r.PhoneNumber,r.Location.PostalCode,r.Location.Town,r.Location.Street,r.Location.Number);
                    restaurantsOutputList.Add(restaurant);
                }
                return restaurantsOutputList;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromSearchRestaurantFreeTableDomain", ex);
            }
        }

        //public static RestaurantsRESTOutputDTOUser MapFromRestaurantDomain(string url, Restaurant restaurant)
        //{
        //    try
        //    {
        //        string restaurantURL = $"{url}/user/{restaurant.Location.PostalCode}/{restaurant.Cuisine}";
        //        RestaurantsRESTOutputDTOUser dto = new RestaurantsRESTOutputDTOUser(restaurantURL, restaurant.RestaurantId,restaurant.Name,restaurant.Cuisine);
        //        return dto;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new MapException("MapFromUserDomain", ex);
        //    }

        //}
    }
}
