using System_Rezerwacji_Biletów.Models;

namespace System_Rezerwacji_Biletów.Repository
{
    public interface IEventRepository
    {
        IQueryable<Event> GetAll();
        Event GetById(int id);
        Event Create(Event event_);
        Task<Event> Update(int id, Event event_);
        Event Delete(int id);
        
    }
}
