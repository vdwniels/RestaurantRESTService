using RestaurantBL.Model;
using RestaurantBL.Services;
using RestaurantRESTServiceUser.Exceptions;
using RestaurantRESTServiceUser.Model.input;
using RestaurantRESTServiceUser.Model.output;

namespace RestaurantRESTServiceUser.Mappers
{
    public class MapFromDomain
    {
        public static UserRESTOutputDTO MapFromUserDomain(string url, User user, UserService userService)
        {
            try
            {
                string userURL = $"{url}/user/{user.CustomerNumber}";
                UserRESTOutputDTO dto = new UserRESTOutputDTO(userURL, user.CustomerNumber, user.Name, user.Email, user.PhoneNumber, user.Location.PostalCode, user.Location.Town, user.Location.Street, user.Location.Number);
                return dto;
            }
            catch(Exception ex)
            {
                throw new MapException("MapFromUserDomain", ex);
            }
        }
    }
}
