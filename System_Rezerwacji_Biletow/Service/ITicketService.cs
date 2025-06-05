using System_Rezerwacji_Biletów.Models;

namespace System_Rezerwacji_Biletów.Service
{
    public interface ITicketService
    {
        IQueryable<Ticket> GetAllTickets();
        Ticket GetTicketById(int id);
        Ticket CreateTicket(Ticket ticket_);
        Task<Ticket> UpdateTicket(int id, Ticket ticket_);
        Ticket DeleteTIcket(int id);
    }
}
