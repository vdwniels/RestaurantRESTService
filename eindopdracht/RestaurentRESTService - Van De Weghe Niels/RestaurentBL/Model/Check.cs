using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Check //TODO fix
    {
        public static bool IsCorrectEmailSyntax(string email)
        {
            //if (string.IsNullOrWhiteSpace(email)) return false;
            //Regex emailCheck = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
            //if (emailCheck.IsMatch(email)) return true;
            //return false;
            return true;
        }

        public static bool IsCorrectPhoneNumberSyntax(string phonenumber)
        {
            //if (string.IsNullOrWhiteSpace(phonenumber)) return false;
            //Regex phoneNumberCheck = new Regex(@"^[\+]?[(]?[0 - 9]{ 3 }[)]?[-\s\.]?[0 - 9]{ 3}[-\s\.]?[0 - 9]{ 4,6}$)", RegexOptions.CultureInvariant | RegexOptions.Singleline);
            //if (phoneNumberCheck.IsMatch(phonenumber)) return true;
            //return false;
            return true;
        }
    }
}
