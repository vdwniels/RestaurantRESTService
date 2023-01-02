using RestaurantBL.Model;

namespace RestaurantRESTServiceUser.Model.output
{
    public class RestaurantsRESTOutputDTOUser
    {
        public RestaurantsRESTOutputDTOUser(int restaurantId, string name, string cuisine, string email, string phoneNumber, int postalCode, string town, string street, string number)
        {
            RestaurantId = restaurantId;
            Name = name;
            Cuisine = cuisine;
            Email = email;
            PhoneNumber = phoneNumber;
            PostalCode = postalCode;
            Town = town;
            Street = street;
            Number = number;
        }

        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public string Cuisine { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PostalCode { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }

    }
}
