using System_Rezerwacji_Biletów.Models;

namespace System_Rezerwacji_Biletów.Data
{
    public static class DbInitializer
    {
        public static void Initializer(SystemReservationContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return;
            }

            var events = new Event[]
            {
                new Event{EventID = 1, NameEvent = "NameEvent", Location = "Location", Date = DateTime.Parse("2025-03-12"), description = "description" }
            };
            foreach (Event e in events)
            {
                context.Events.Add(e);
            }
            context.SaveChanges();

            var tickets = new Ticket[]
            {
                new Ticket{TicketID = 1, EventID = 1, price = 1000}
            };
            foreach (Ticket t in tickets)
            {
                context.Tickets.Add(t);
            }
            context.SaveChanges();


            var users = new User[]
            {
                new User{UserID = 1, BirthDate = DateTime.Parse("1999-05-21"), LastName = "LastName",  Name = "name", email = "email" },
                new User{UserID = 2, BirthDate = DateTime.Parse("1999-05-21"), LastName = "LastName",  Name = "name", email = "email" }
            };
            foreach(User s in users)
            {
                context.Users.Add(s);
            }
            context.SaveChanges();

            var reservations = new Reservation[]
            {
                new Reservation{ReservationID = 1, TicketID = 1,TotalPrice = 1000, UserID = 1 }
            };
            foreach (Reservation r in reservations)
            {
                context.Reservations.Add(r);
            }
            context.SaveChanges();

            

            

        }
    }
}
