namespace RestaurantRESTServiceUser.Model.output
{
    public class TableRESTOutputDTOAdmin
    {
        public TableRESTOutputDTOAdmin(int tableId, int tableNumber, int seats, int restaurantId)
        {
            TableId = tableId;
            TableNumber = tableNumber;
            Seats = seats;
            RestaurantId = restaurantId;
        }

        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public int Seats { get; set; }
        public int RestaurantId { get; set; }

    }
}
