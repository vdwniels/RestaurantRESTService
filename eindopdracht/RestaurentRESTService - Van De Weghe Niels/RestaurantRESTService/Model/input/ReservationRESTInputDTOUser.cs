namespace RestaurantRESTServiceUser.Model.input
{
    public class ReservationRESTInputDTOUser
    {
        public int RestaurantId { get; set; }   
        public int CustomerId { get; set; }
        public int Seats { get; set; }
        public DateTime DateAndHour { get; set; }
    }
}
