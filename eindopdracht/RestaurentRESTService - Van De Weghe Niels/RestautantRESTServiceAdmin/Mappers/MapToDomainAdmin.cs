using RestaurantBL.Model;
using RestaurantRESTServiceAdmin.Exceptions;
using RestaurantRESTServiceAdmin.Model.input;
using RestautantRESTServiceAdmin.Model.input;

namespace RestaurantRESTServiceUser.Mappers
{
    public class MapToDomainAdmin
    {
        public static Restaurant MapToRestaurantDomain(RestaurantRESTInputDTOAdmin dto)
        {
            try
            {
                Location location = new Location(dto.Location.PostalCode, dto.Location.Town);
                if(!string.IsNullOrWhiteSpace(dto.Location.Street) && dto.Location.Street != "string") location.SetStreet(dto.Location.Street);
                if (!string.IsNullOrWhiteSpace(dto.Location.Number) && dto.Location.Number != "string") location.SetNumber(dto.Location.Number);
                Restaurant restaurant  = new Restaurant(dto.Name,location,dto.Cuisine, dto.Email, dto.PhoneNumber);
                return restaurant;
            }
            catch (Exception ex)
            {
                throw new MapException("MapToRestaurantDomain", ex);
            }
        }

        public static Table MapToTableDomain(TableRESTInputDTOAdmin dto)
        {
            try
            {
                Table t = new Table(dto.TableNumber, dto.Seats, dto.RestaurantId);
                return t;
            }
            catch (Exception ex)
            {
                throw new MapException("MapToTableDomain", ex);
            }
        }

    }
}
