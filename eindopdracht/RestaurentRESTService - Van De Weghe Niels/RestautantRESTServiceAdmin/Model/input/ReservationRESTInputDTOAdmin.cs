namespace RestaurantRESTServiceAdmin.Model.input
{
    public class ReservationRESTInputDTOAdmin
    {
        public int RestaurantId { get; set; }   
        public int CustomerId { get; set; }
        public int Seats { get; set; }
        public DateTime DateAndHour { get; set; }
    }
}
