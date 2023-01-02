namespace RestautantRESTServiceAdmin.Model.output
{
    public class TableRESTOutputDTOAdmin
    {
        public TableRESTOutputDTOAdmin(int tableId, int tableNumber, int seats)
        {
            TableId = tableId;
            TableNumber = tableNumber;
            Seats = seats;
        }

        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public int Seats { get; set; }

    }
}
