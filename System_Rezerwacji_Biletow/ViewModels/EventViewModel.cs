using System.ComponentModel.DataAnnotations;

namespace System_Rezerwacji_Biletów.ViewModels
{
    public class EventViewModel
    {
        public int EventID { get; set; }

        [Required(ErrorMessage = "Please enter event name")]
        public string NameEvent { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter yours birth date")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter location")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Pleare enter description")]
        public string description { get; set; }
    }
}
