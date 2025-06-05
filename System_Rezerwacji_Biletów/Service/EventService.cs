using Microsoft.Build.Framework;
using System_Rezerwacji_Biletów.Models;
using System_Rezerwacji_Biletów.Repository;

namespace System_Rezerwacji_Biletów.Service
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;

        public EventService(IEventRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        public IQueryable<Event> GetAllEvents()
        {
            return _eventRepository.GetAll();
        }
        public Event GetEventById(int id)
        {
            return _eventRepository.GetById(id);
        }
        public Event CreateEvent(Event event_)
        {
            return _eventRepository.Create(event_);
        }
        public async Task<Event> UpdateEvent(int id, Event event_)
        {
            return await _eventRepository.Update(id, event_);
        }

        public Event DeleteEvent(int id)
        {
            return _eventRepository.Delete(id);
        }
    }
}
