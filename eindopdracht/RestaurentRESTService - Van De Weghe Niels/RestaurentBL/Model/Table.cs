using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Table
    {
        public Table(int tableNumber, int seats, int restaurantId)
        {
            SetTableNumber(tableNumber);
            SetSeats(seats);
            SetRestaurantId(restaurantId);
        }

        public Table(int tableId, int tableNumber, int seats, int restaurantId) : this (tableNumber, seats, restaurantId)
        {
            SetTableId(tableId);
        }

        public int TableId { get; set; }
        public int TableNumber { get;set; }
        public int Seats { get;set; }
        public int RestaurantId { get; set; }

        public void SetTableId(int id)
        {
            if (id < 1) throw new TableException("Table - SetTableId - No negative ID's");
            TableId = id;
        }

        public void SetTableNumber(int tablenr)
        {
            if (tablenr < 1) throw new TableException("Table - SetTableNumber - No negative table numbers");
            TableNumber = tablenr;
        }

        public void SetSeats (int seats)
        {
            if (seats < 1) throw new TableException("Table - SetSeats - Must have at least 1 seat");
            Seats = seats;
        }

        public void SetRestaurantId(int restaurantId)
        {
            if (restaurantId < 1) throw new TableException("Table - SetRestaurantId - No negative ID's");
            RestaurantId = restaurantId;
        }

        public override bool Equals(object? obj)
        {
            return obj is Table table &&
                   TableId == table.TableId &&
                   TableNumber == table.TableNumber &&
                   Seats == table.Seats &&
                   RestaurantId == table.RestaurantId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TableId, TableNumber, Seats, RestaurantId);
        }

        public static bool operator ==(Table t1, Table t2)
        {
            if ((object)t1 == null)
                return (object)t2 == null;
            return t1.Equals(t2);
        }
        public static bool operator !=(Table t1, Table t2)
        {
            return !(t1 == t2);
        }

    }
}
