namespace System_Rezerwacji_Biletów.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public int EventID { get; set; }
        public int ReservationID { get; set; }
        public float price { get; set; }

        public Event Event { get; set; }
        public Reservation Reservation { get; set; }
    }
}
