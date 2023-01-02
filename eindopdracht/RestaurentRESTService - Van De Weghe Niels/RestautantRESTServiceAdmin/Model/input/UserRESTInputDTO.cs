using RestaurantBL.Model;

namespace RestaurantRESTServiceAdmin.Model.input
{
    public class UserRESTInputDTO
    {
        public string Name { get; set; }
        public LocationRESTInputDTOAdmin Location { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


    }
}
