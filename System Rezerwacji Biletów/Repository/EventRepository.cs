 using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System_Rezerwacji_Biletów.Repository;
using System_Rezerwacji_Biletów.Data;
using System_Rezerwacji_Biletów.Models;

public class EventRepository : IEventRepository
{
    private readonly SystemReservationContext _context;

    public EventRepository(SystemReservationContext context)
    {
        _context = context;
    }

    public IQueryable<Event> GetAll()
    {
        return _context.Events.AsQueryable();
    }

    public Event GetById(int id)
    {
        return  _context.Events.Find(id);
        
    }

    public Event Create(Event event_)
    {
         _context.Events.Add(event_);
         _context.SaveChanges();

        return event_;
    }

    public async Task<Event> Update(int id, Event event_)
    {
        var existingEvent = await _context.Events.FindAsync(id);

        existingEvent.NameEvent = event_.NameEvent;
        existingEvent.Date = event_.Date;
        existingEvent.Location = event_.Location;
        existingEvent.description = event_.description;

        await _context.SaveChangesAsync();
        return existingEvent;
    }

    public Event Delete(int id)
    {
        var eventModel =  _context.Events.Find(id);

        _context.Events.Remove(eventModel);
         _context.SaveChanges();

        return eventModel;
    }
    
    
}
