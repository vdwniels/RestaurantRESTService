using RestaurantBL.Model;
using RestaurantRESTServiceUser.Exceptions;
using RestaurantRESTServiceUser.Model.input;

namespace RestaurantRESTServiceUser.Mappers
{
    public class MapToDomain
    {
        public static User MapToUserDomain(UserRESTInputDTO dto)
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
    }
}
