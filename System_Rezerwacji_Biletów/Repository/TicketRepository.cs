using Microsoft.EntityFrameworkCore;
using System_Rezerwacji_Biletów.Data;
using System_Rezerwacji_Biletów.Models;

namespace System_Rezerwacji_Biletów.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly SystemReservationContext _context;

        public TicketRepository(SystemReservationContext context)
        {
            _context = context;
        }
        public IQueryable<Ticket> GetAll()
        {
            return _context.Tickets.Include(t=>t.Event).AsQueryable();
        }
        public Ticket GetById(int id)
        {
            return _context.Tickets.Include(t=>t.Event).FirstOrDefault(t=>t.TicketID == id); 
        }
        public Ticket Create(Ticket ticket_)
        {
            _context.Tickets.Add(ticket_);
            _context.SaveChanges();
            return ticket_;
        }
        public async Task<Ticket> Update(int id, Ticket ticket_)
        {
            var existTicket = await _context.Tickets.FindAsync(id);

            existTicket.TicketID = ticket_.TicketID;
            existTicket.price = ticket_.price;
            existTicket.EventID = ticket_.EventID;

            await _context.SaveChangesAsync();
            return existTicket;
        }
        public Ticket Delete(int id)
        {
            var ticketModel = _context.Tickets.Find(id);

            _context.Tickets.Remove(ticketModel);
            _context.SaveChanges();
            return ticketModel;
        } 
    }
}
