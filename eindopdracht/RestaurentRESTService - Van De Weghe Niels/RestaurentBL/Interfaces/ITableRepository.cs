using RestaurantBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Interfaces
{
    public interface ITableRepository
    {
        List<Table> GetAllTablesOfRestaurant(int restaurantId);
    }
}
