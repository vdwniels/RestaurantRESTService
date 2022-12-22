using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Table
    {
        public Table(int tableNumber, int seats)
        {
            TableNumber = tableNumber;
            Seats = seats;
        }

        public Table(int tableId, int tableNumber, int seats) : this(tableNumber, seats)
        {
            TableId = tableId;
        }

        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public int Seats { get; set; }


        public override bool Equals(object? obj)
        {
            return obj is Table table &&
                   TableNumber == table.TableNumber &&
                   Seats == table.Seats;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TableNumber, Seats);
        }
    }
}
