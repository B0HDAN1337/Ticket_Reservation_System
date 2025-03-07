using System_Rezerwacji_Biletów.Models;

namespace System_Rezerwacji_Biletów.Data
{
    public static class DbInitializer
    {
        public static void Initializer(SystemReservationContext context)
        {
            context.Database.EnsureCreated();

           

            

            

        }
    }
}
