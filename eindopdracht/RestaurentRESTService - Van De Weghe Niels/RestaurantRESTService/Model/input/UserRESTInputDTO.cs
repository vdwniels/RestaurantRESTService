using RestaurantBL.Model;

namespace RestaurantRESTServiceUser.Model.input
{
    public class UserRESTInputDTO
    {
        public string Name { get; set; }
        public LocationRESTInputDTO Location { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


    }
}
