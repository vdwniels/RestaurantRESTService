using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Location
    {
        public Location(int postalCode, string town)
        {
            SetPostalCode(postalCode);
            SetTown(town);
        }

        public int PostalCode { get; set; }
        public string Town { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }

        public void SetPostalCode(int postalCode)
        {
            if (postalCode.ToString().Length != 4) throw new LocationException("Location - SetPostalCode - Postalcode must be 4 digits long.");
            PostalCode = postalCode;
        }

        public void SetTown(string town)
        {
            if (string.IsNullOrWhiteSpace(town)) throw new LocationException("Location - SetTown - No town entry");
            town = town.Trim();
            Town = char.ToUpper(town[0]) + town.Substring(1).ToLower();
        }

        public void SetStreet(string street)
        {
            if (string.IsNullOrWhiteSpace(street)) throw new LocationException("Location - SetStreet - No street entry");
            street = street.Trim();
            Street = char.ToUpper(street[0]) + street.Substring(1).ToLower();
        }

        public void SetNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) throw new LocationException("Location - SetNumber - No number entry");
            Number = number.Trim();
        }

        public override string ToString()
        {
            string s = $"";
            if (!string.IsNullOrEmpty(Street)) s += $"{Street} ";
            if (!string.IsNullOrEmpty(Number)) s += $"{Number} ";
            s += $"{PostalCode} {Town}";
            return s;
        }
    }
}
