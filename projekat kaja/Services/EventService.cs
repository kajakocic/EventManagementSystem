using projekat_kaja.Models;
using projekat_kaja.Repositories;

namespace projekat_kaja.Services;

public class EventService : IEventService
{
    private readonly IEventRepository EventRepository;
    public EventService(IEventRepository eventRepository)
    {
        EventRepository = eventRepository;
    }
    public Event AddEvent(Event ev)
    {
        EventRepository.Add(ev);
        EventRepository.SaveChanges();
        return ev;
    }

    public void DeleteEvent(int id)
    {
        EventRepository.Delete(id);
        EventRepository.SaveChanges();
    }

    public IEnumerable<Event> FilterEvents(DateTime? datum = null, TimeSpan? vreme = null, string? kategorija = null, string? lokacija = null)
    {
        //ispravi ovo da logika bude u servisu
        /* var allEvents = EventRepository.GetQueryabvle();

        if (datum.HasValue)
        {
            allEvents = allEvents.Where(e => e.Datum.Date == datum.Value.Date);
        }
        if (vreme.HasValue)
        {
            query = query.Where(e => e.Vreme == vreme.Value);
        }
        if (!string.IsNullOrWhiteSpace(kategorija))
        {
            query = query.Where(e => e.KategorijaEvent != null && e.KategorijaEvent.Naziv == kategorija);
        }
        if (!string.IsNullOrWhiteSpace(lokacija))
        {
            query = query.Where(e => e.LocationEvent != null && e.LocationEvent.Naziv == lokacija);
        } */

        return EventRepository.FilterAllEvents(datum, vreme, kategorija, lokacija);
    }

    public IEnumerable<Event> GetAllEvents()
    {
        return EventRepository.GetAll();
    }

    public Event GetEventById(int id)
    {
        return EventRepository.Get(id);
    }

    public Event UpdateEvent(Event ev)
    {
        EventRepository.Update(ev);
        EventRepository.SaveChanges();
        return ev;
    }
}