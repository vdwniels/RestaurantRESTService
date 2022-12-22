using RestaurantBL.Interfaces;
using RestaurantBL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Services
{
    public class TableService
    {
        private ITableRepository repo;

        public TableService(ITableRepository repo)
        {
            this.repo = repo;
        }

        public List<Table> GetAllTablesOfRestaurant(int restaurantId)
        {
            List<Table> tables = new List<Table>();
            tables = repo.GetAllTablesOfRestaurant(restaurantId);
            return tables;
        }
    }
}
