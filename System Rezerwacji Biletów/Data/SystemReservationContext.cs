using System_Rezerwacji_Biletów.Models;
using Microsoft.EntityFrameworkCore;

namespace System_Rezerwacji_Biletów.Data
{
    public class SystemReservationContext : DbContext
    {
        public SystemReservationContext(DbContextOptions<SystemReservationContext> options) : base(options)
        {

        }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<Ticket>().ToTable("Ticket");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Event>().ToTable("Event");
        }
    }
}
