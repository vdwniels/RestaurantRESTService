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
        public Table(int tableNumber, int seats)
        {
            TableNumber = tableNumber;
            Seats = seats;
        }

        public int TableNumber { get;set; }
        public int Seats { get;set; }

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
