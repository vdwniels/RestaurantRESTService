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
        Table AddTable(Table table);
        void DeleteTable(int tableId);
        Table GetTable(int tableId);
        bool TableExists(int tableNumber, int restaurantId);
        bool TableExists(int tableId);
        void UpdateTable(Table table);
    }
}
