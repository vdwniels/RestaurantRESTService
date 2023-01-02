namespace RestaurantRESTServiceUser.Model.output
{
    public class ReservationRESTOutputDTOUser
    {

        public ReservationRESTOutputDTOUser(int reservationId, int tableNumber, int restaurantId, int customerId, int seats, DateTime dateAndHour)
        {
            ReservationId = reservationId;
            TableNumber = tableNumber;
            RestaurantId = restaurantId;
            CustomerId = customerId;
            Seats = seats;
            DateAndHour = dateAndHour;
        }

        public int ReservationId { get; set; }
        public int TableNumber { get; set; }
        public int RestaurantId { get; set; }
        public int CustomerId { get; set; }
        public int Seats { get; set; }
        public DateTime DateAndHour { get; set; }

    }
}
