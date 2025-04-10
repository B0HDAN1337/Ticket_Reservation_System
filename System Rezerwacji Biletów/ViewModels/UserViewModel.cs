using System.ComponentModel.DataAnnotations;

namespace System_Rezerwacji_Biletów.ViewModels
{
    public class UserViewModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter last Name")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please enter yours birth date")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Please enter email")]
        public string email { get; set; }
    }
}


