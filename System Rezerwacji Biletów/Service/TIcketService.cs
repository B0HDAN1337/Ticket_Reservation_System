using System_Rezerwacji_Biletów.Models;
using System_Rezerwacji_Biletów.Repository;

namespace System_Rezerwacji_Biletów.Service
{
    public class TIcketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TIcketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public IQueryable<Ticket> GetAllTickets()
        {
            return _ticketRepository.GetAll();
        }
        public Ticket GetTicketById(int id)
        {
            return _ticketRepository.GetById(id);
        }
        public Ticket CreateTicket(Ticket ticket_)
        {
            return _ticketRepository.Create(ticket_);
        }
        public async Task<Ticket> UpdateTicket(int id, Ticket ticket_)
        {
            return await _ticketRepository.Update(id, ticket_);
        }
        public Ticket DeleteTIcket(int id)
        {
            return _ticketRepository.Delete(id);
        }
    }
}
