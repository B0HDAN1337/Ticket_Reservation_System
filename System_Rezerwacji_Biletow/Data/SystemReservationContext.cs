using System_Rezerwacji_Biletów.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace System_Rezerwacji_Biletów.Data
{
    public class SystemReservationContext : IdentityDbContext
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
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<Ticket>().ToTable("Ticket");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Event>().ToTable("Event");

            modelBuilder.Entity<User>()
            .HasMany(u => u.Reservations)
            .WithOne(r => r.User)
            .HasForeignKey(r => r.UserID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Reservation>()
            .HasMany(r => r.Tickets)
            .WithOne(t => t.Reservation)
            .HasForeignKey(t => t.ReservationID)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Event>()
            .HasMany(e => e.Tickets)
            .WithOne(t => t.Event)
            .HasForeignKey(t => t.EventID)
            .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
