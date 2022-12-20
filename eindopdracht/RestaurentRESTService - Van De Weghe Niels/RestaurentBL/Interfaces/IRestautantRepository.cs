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
        IReadOnlyList<Restaurant> GetRestaurantsWithFreeTables(DateTime date, int seats);
        bool RestaurantExists(string phoneNumber, string email);
        IReadOnlyList<Restaurant> SearchRestaurantOnLocationAndOrCuisine(int? postalcode, string? cuisine);
    }
}
