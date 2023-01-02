namespace RestautantRESTServiceAdmin.Model.output
{
    public class RestaurantRESTOutputDTOAdmin
    {
        public RestaurantRESTOutputDTOAdmin(int restaurantId, string name, string cuisine, string email, string phoneNumber, int postalCode, string town, string street, string number)
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
        public string Cuisine {get; set;}
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PostalCode { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public List<TableRESTOutputDTOAdmin> _tables = new List<TableRESTOutputDTOAdmin>();

        public void AddTable (TableRESTOutputDTOAdmin t)
        {
            _tables.Add (t);
        }
    }
}
