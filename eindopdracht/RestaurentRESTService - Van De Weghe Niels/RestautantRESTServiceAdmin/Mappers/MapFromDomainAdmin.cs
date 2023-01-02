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
                    dto.AddTable(new TableRESTOutputDTOAdmin(t.TableId, t.TableNumber, t.Seats));
                }
                return dto;
            }
            catch (Exception ex)
            {
                throw new MapException("MapFromUserDomain", ex);
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
                throw new MapException("MapFromUserDomain", ex);
            }
        }
    }
}
