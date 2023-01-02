using RestaurantBL.Model;
using RestaurantRESTServiceAdmin.Model.input;

namespace RestautantRESTServiceAdmin.Model.input
{
    public class RestaurantRESTInputDTOAdmin
    {
        public string Name { get; set; }
        public LocationRESTInputDTOAdmin Location { get; set; }
        public string Cuisine { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

    }
}
