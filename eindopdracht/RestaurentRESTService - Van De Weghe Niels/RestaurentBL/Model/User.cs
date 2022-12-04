using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class User
    {
        public User(int customerNumber, string name, string email, string phoneNumber, Location location) : this (name,email,phoneNumber,location)
        {
            CustomerNumber = customerNumber;
        }

        public User(string name, string email, string phoneNumber, Location location)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            Location = location;
        }


        public int CustomerNumber { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
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

        public void SetEmail (string email)
        {
            email = email.Trim().ToLower();
            if (!Check.IsCorrectEmailSyntax(email)) throw new UserException("User - SetEmail - Email incorrect (syntax)");
            Email = email;
        }

        public void SetPhoneNumber (string phonenumber)
        {
            phonenumber = phonenumber.Trim();
            if (!Check.IsCorrectPhoneNumberSyntax(phonenumber)) throw new UserException("User - SetPhoneNumber - Phonenumber incorrect (syntax)");
            PhoneNumber = phonenumber;
        }
        
        public void SetLocation (Location location)
        {
            if (Location == null) throw new UserException("User - SetLocation - No location entry");
            Location = location;
        }
    }
}
