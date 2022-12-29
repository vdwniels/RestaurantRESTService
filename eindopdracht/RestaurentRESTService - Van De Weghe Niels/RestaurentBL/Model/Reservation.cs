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
        public Reservation(int reservationNumber,int tablenumber, Restaurant restaurantInfo, User customer, int seats, DateTime dateAndHour) : this (restaurantInfo, customer, seats, dateAndHour)
        {
            SetReservationNumber(reservationNumber);
            SetTableNumber(tablenumber);
        }
        public Reservation(Restaurant restaurantInfo, User customer, int seats, DateTime dateAndHour)
        {
            SetRestaurantInfo(restaurantInfo);
            SetCustomer(customer);
            SetSeats(seats);
            SetDateAndHour(dateAndHour);
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
            if (this.RestaurantInfo._tables.Count > 0 && !this.RestaurantInfo._tables.ContainsValue(tableNumber)) throw new ReservationException("Reservation - SetTableNumber - Restaurant doesn't contain a table with this table number ");
            Tablenumber = tableNumber;
        }

        public void SetSeats(int seats)
        {
            if (seats < 1) throw new ReservationException("Reservation - SetSeats - must have at least one chair");
            if (this.Tablenumber != 0 && seats > this.RestaurantInfo._tables.FirstOrDefault(x => x.Value == this.Tablenumber).Key.Seats) throw new ReservationException("Reservation - SetSeats - Table not big enough");
            Seats = seats;
        }

        public void SetDateAndHour(DateTime dateAndHour)
        {
            if (dateAndHour == null) throw new ReservationException("Reservation - SetDateAndHour - no DateAndHour entry");
            if (dateAndHour < DateTime.Now) throw new ReservationException("Reservation - SetDateAndHour - Can't make reservation in the past");
            if (dateAndHour.Minute != 30 && dateAndHour.Minute != 00 ) throw new ReservationException("Reservation - SetDateAndHour - Can only make a reservation every other half hour (e.g. 20:00 and 20:30)");
            DateAndHour = dateAndHour;
        }

        public override bool Equals(object? obj)
        {
            return obj is Reservation reservation &&
                   ReservationNumber == reservation.ReservationNumber &&
                   EqualityComparer<Restaurant>.Default.Equals(RestaurantInfo, reservation.RestaurantInfo) &&
                   EqualityComparer<User>.Default.Equals(Customer, reservation.Customer) &&
                   Tablenumber == reservation.Tablenumber &&
                   Seats == reservation.Seats &&
                   DateAndHour == reservation.DateAndHour;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ReservationNumber, RestaurantInfo, Customer, Tablenumber, Seats, DateAndHour);
        }

        public static bool operator ==(Reservation r1, Reservation r2)
        {
            if ((object)r1 == null)
                return (object)r2 == null;
            return r1.Equals(r2);
        }
        public static bool operator !=(Reservation r1, Reservation r2)
        {
            return !(r1 == r2);
        }

    }
}
