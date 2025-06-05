
namespace System_Rezerwacji_Biletów.Models
{
    public class Reservation
    {
        public int ReservationID { get; set; }
        public int UserID { get; set; }
        public float TotalPrice { get; set; }

        public User User { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
