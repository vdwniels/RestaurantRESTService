using Microsoft.VisualBasic;
using RestaurantBL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantBL.Model
{
    public class Reservation
    {
        public Reservation(Restaurant restaurantInfo, User customer, int seats, int tablenumber, DateTime dateAndHour)
        {
            SetRestaurantInfo(restaurantInfo);
            SetCustomer(customer);
            SetTableNumber(tablenumber);
            SetSeats(seats);
            SetDateAndHour(dateAndHour);
        }

        public Reservation(int reservationNumber, Restaurant restaurantInfo, User customer, int seats, int tablenumber, DateTime dateAndHour) : this (restaurantInfo, customer, seats, tablenumber, dateAndHour)
        {
            SetReservationNumber(reservationNumber);
        }

        public int ReservationNumber { get; set; }
        public Restaurant RestaurantInfo { get; set; }
        public User Customer { get; set; }
        public int Tablenumber { get; set; }
        public int Seats { get; set; }
        public DateTime DateAndHour { get; set; }

        public void SetReservationNumber(int reservationNumber)
        {
            if (reservationNumber < 1) throw new ReservationException("Reservation - SetReservationNumber - reservationnumber must be larger than 1");
            ReservationNumber = reservationNumber;
        }

        public void SetRestaurantInfo(Restaurant restaurantInfo)
        {
            if (restaurantInfo == null) throw new ReservationException("Reservation - SetRestaurantInfo - no restaurantinfo entry");
            RestaurantInfo = restaurantInfo;
        }

        public void SetCustomer(User customer)
        {
            if (customer == null) throw new ReservationException("Reservation - SetCustomerInfo - no customer entry");
            Customer = customer;
        }

        public void SetTableNumber(int tableNumber)
        {
            if (tableNumber < 1) throw new ReservationException("Reservation - SetTableNumber - TableNumber less than 1");
            if (!this.RestaurantInfo._tables.ContainsValue(tableNumber)) throw new ReservationException("Reservation - SetTableNumber - Restaurant doesn't contain a table with this table number ");
            Tablenumber = tableNumber;
        }

        public void SetSeats(int seats)
        {
            if (seats < 1) throw new ReservationException("Reservation - SetSeats - must have at least one chair");
            if (seats > this.RestaurantInfo._tables.FirstOrDefault(x => x.Value == this.Tablenumber).Key.Seats) throw new ReservationException("Reservation - SetSeats - Table not big enough");
            Seats = seats;
        }

        public void SetDateAndHour(DateTime dateAndHour)
        {
            if (dateAndHour == null) new ReservationException("Reservation - SetDateAndHour - no DateAndHour entry");
            if (dateAndHour > DateTime.Now) new ReservationException("Reservation - SetDateAndHour - Can't make reservation in the past");
            DateAndHour = dateAndHour;
        }
    }
}
