using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class ContactDetails
    {
        public ContactDetails(string email, string phoneNumber)
        {
            SetEmail(email);
            SetPhoneNumber(phoneNumber);
        }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public void SetEmail(string email)
        {
            email = email.Trim().ToLower();
            if (!Check.IsCorrectEmailSyntax(email)) throw new ContactDetailsException("ContactDetails - SetEmail - Email incorrect (syntax)");
            Email = email;
        }

        public void SetPhoneNumber(string phonenumber)
        {
            phonenumber = phonenumber.Trim();
            if (!Check.IsCorrectPhoneNumberSyntax(phonenumber)) throw new ContactDetailsException("ContactDetails - SetPhoneNumber - Phonenumber incorrect (syntax)");
            PhoneNumber = phonenumber;
        }

    }
}
