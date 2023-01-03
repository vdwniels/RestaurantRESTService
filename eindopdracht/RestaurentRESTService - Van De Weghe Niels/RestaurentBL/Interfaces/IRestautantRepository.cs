using RestaurantBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Interfaces
{
    public interface IRestautantRepository
    {
        Restaurant AddRestaurant(Restaurant restaurant);
        void DeleteAllTables(int restaurantId);
        void DeleteRestaurant(int restaurantId);
        Restaurant GetRestaurant(int restaurantId);
        IReadOnlyList<Restaurant> GetRestaurantsWithFreeTables(DateTime date, int seats);
        bool HasTables(int restaurantId);
        bool RestaurantExists(string phoneNumber, string email);
        bool RestaurantExists(int restaurantId);
        IReadOnlyList<Restaurant> SearchRestaurantOnLocationAndOrCuisine(int? postalcode, string? cuisine);
        void UpdateRestaurant(Restaurant restaurant);
    }
}
