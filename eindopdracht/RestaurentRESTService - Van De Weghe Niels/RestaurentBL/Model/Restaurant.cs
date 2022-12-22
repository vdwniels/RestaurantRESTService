using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Restaurant : ContactDetails
    {
        public Dictionary<Table,int> _tables = new Dictionary<Table,int>();

        public Restaurant(string name, Location location, string cuisine, string email, string phonenumber) : base (email,phonenumber)
        {
            SetName(name);
            SetLocation(location);
            SetCuisine(cuisine);
        }

        public Restaurant(int restaurantId, string name, Location location, string cuisine, string email, string phonenumber) : this (name, location, cuisine,  email, phonenumber)
        {
            SetRestaurantId(restaurantId);
        }

        public int RestaurantId { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Cuisine { get; set; }

        public void SetRestaurantId (int id)
        {
            if (id < 1) throw new RestaurantException("Restaurant - SetRestaurantId - id is less than 1");
            RestaurantId = id;
        }

        public void SetName (string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new RestaurantException("Restaurant - SetName - No name entry");
            Name = name.Trim();
        }

        public void SetLocation (Location location)
        {
            if (Location == null) throw new RestaurantException("Restaurant - SetLocation - No location entry");
            Location = location;
        }

        public void SetCuisine (string cuisine)
        {
            if (string.IsNullOrWhiteSpace(cuisine)) throw new RestaurantException("Restaurant - SetCuisine - No cuisine entry");
            cuisine = cuisine.Trim();
            Cuisine = char.ToUpper(cuisine[0]) + cuisine.Substring(1).ToLower();
        }

        public IReadOnlyDictionary<Table,int> GetTables()
        {
            return _tables;
        }

        public void AddTable (Table table)
        {
            if (table == null) throw new RestaurantException("Restaurant - AddTable - No table entry");
            foreach (Table t in _tables.Keys)
            {
                if (t.TableNumber == table.TableNumber) throw new RestaurantException("Restaurant - AddTable - Table already exists");
            }
            _tables.Add(table,table.TableNumber);
        }

        public void DeleteTable(Table table)
        {
            if (table == null) throw new RestaurantException("Restaurant - DeleteTable - No table entry");
            if (_tables.Keys.Contains(table))
            {
                _tables.Remove(table);
            }
            else throw new RestaurantException("Restaurant - DeleteTable - Table doesn't exist");
        }

        public override bool Equals(object? obj)
        {
            return obj is Restaurant restaurant &&
                   Email == restaurant.Email &&
                   PhoneNumber == restaurant.PhoneNumber &&
                   RestaurantId == restaurant.RestaurantId &&
                   Name == restaurant.Name &&
                   EqualityComparer<Location>.Default.Equals(Location, restaurant.Location) &&
                   Cuisine == restaurant.Cuisine;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Email, PhoneNumber, RestaurantId, Name, Location, Cuisine);
        }

        public static bool operator ==(Restaurant r1, Restaurant r2)
        {
            if ((object)r1 == null)
                return (object)r2 == null;
            return r1.Equals(r2);
        }
        public static bool operator !=(Restaurant r1, Restaurant r2)
        {
            return !(r1 == r2);
        }

    }
}
