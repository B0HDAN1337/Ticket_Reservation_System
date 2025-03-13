namespace System_Rezerwacji_Biletów.Models
{
    public class Ticket
    {
        public int TicketID { get; set; }
        public int EventID { get; set; }
        public float price { get; set; }

        public ICollection<Reservation> Reservations { get; set; }

        public Event Event { get; set; }
    }
}
