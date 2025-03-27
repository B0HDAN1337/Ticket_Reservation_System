
namespace System_Rezerwacji_Biletów.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int TicketID { get; set; }
        public int UserID { get; set; }
        public float TotalPrice { get; set; }

        public Ticket Ticket { get; set; }
        public User User { get; set; }
    }
}
