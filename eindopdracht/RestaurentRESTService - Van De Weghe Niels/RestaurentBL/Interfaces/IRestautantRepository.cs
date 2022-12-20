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
        IReadOnlyList<Restaurant> GetRestaurantsWithFreeTables(DateTime date, int seats);
        IReadOnlyList<Restaurant> SearchRestaurantOnLocationAndOrCuisine(int? postalcode, string? cuisine);
    }
}
