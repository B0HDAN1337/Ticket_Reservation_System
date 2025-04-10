using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace System_Rezerwacji_Biletów.ViewModels
{
    public class TicketViewModel
    {
        public int TicketID { get; set; }

        [Required(ErrorMessage = "Enter event")]
        public int EventID { get; set; }

        public float price { get; set; }

        public string? NameEvent { get; set; }

        public IEnumerable<SelectListItem>? Events { get; set; }
    }    
}


