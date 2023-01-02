namespace RestaurantRESTServiceUser.Model.output
{
    public class UserRESTOutputDTOUser
    {
        public UserRESTOutputDTOUser(string id, int customerNumber, string name, string email, string phoneNumber, int postalCode, string town, string street, string number)
        {
            Id = id;
            CustomerNumber = customerNumber;
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            PostalCode = postalCode;
            Town = town;
            Street = street;
            Number = number;
        }
        public string Id { get; set; }
        public int CustomerNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int PostalCode { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }


    }
}
