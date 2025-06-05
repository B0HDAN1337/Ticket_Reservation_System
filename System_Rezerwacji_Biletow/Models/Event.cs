using System.ComponentModel.Design.Serialization;

namespace System_Rezerwacji_Biletów.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string NameEvent { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public string description { get; set; }

        public ICollection<Ticket> Tickets { get; set; }
    }
}
