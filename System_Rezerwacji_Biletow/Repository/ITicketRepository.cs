using System_Rezerwacji_Biletów.Models;
namespace System_Rezerwacji_Biletów.Repository
{
    public interface ITicketRepository
    {
        IQueryable<Ticket> GetAll();
        Ticket GetById(int id);
        Ticket Create(Ticket ticket_);
        Task<Ticket> Update(int id, Ticket ticket_);
        Ticket Delete(int id);
    }
}
