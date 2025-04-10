using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System_Rezerwacji_Biletów.Models;
namespace System_Rezerwacji_Biletów.ViewModels
{
    public class TicketViewModel
    {
        public int TicketID { get; set; }

        [Required(ErrorMessage = "Enter event")]
        public int EventID { get; set; }

        [Range(0.01, 1000, ErrorMessage = "Price must be > 0")]
        public float price { get; set; }

        public string? NameEvent { get; set; }

        public IEnumerable<SelectListItem>? Events { get; set; }
    }    
}


