using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class User : ContactDetails
    {
        public User(int customerNumber, string name, string email, string phoneNumber, Location location) : this (name,email,phoneNumber,location)
        {
            SetCustomerNumber (customerNumber);
        }

        public User(string name, string email, string phoneNumber, Location location) : base (email,phoneNumber)
        {
            SetName (name);
            SetLocation (location);
        }


        public int CustomerNumber { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }

        public void SetCustomerNumber (int customerNumber)
        {
            if (customerNumber < 1) throw new UserException("User - SetCustomerNumber - customernumber less than 1");
            CustomerNumber = customerNumber;
        }

        public void SetName (string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new UserException("User - SetName - No name entry");
            Name = name.Trim();
        }

        
        public void SetLocation (Location location)
        {
            if (location == null) throw new UserException("User - SetLocation - No location entry");
            Location = location;
        }

        public override string ToString()
        {
            return $"User: {CustomerNumber} - {Name} - {Email} - {PhoneNumber} - {Location.PostalCode} - {Location.Town} - {Location.Street} - {Location.Number}";
        }
    }
}
