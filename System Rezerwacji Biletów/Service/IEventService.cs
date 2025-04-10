using System_Rezerwacji_Biletów.Models;

namespace System_Rezerwacji_Biletów.Service
{
    public interface IEventService
    {
        IQueryable<Event> GetAllEvents();
        Event GetEventById(int id);
        Event CreateEvent(Event event_);
        Task<Event> UpdateEvent(int id, Event event_);
        Event DeleteEvent(int id);
    }
}
