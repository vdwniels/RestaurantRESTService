using RestaurantBL.Model;

namespace RestaurantRESTServiceUser.Model.input
{
    public class UserRESTInputDTOUser
    {
        public string Name { get; set; }
        public LocationRESTInputDTOUser Location { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


    }
}
