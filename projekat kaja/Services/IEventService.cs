using projekat_kaja.Models;

namespace projekat_kaja.Services;
public interface IEventService
{
    Event AddEvent(Event ev);
    IEnumerable<Event> GetAllEvents();
    Event GetEventById(int id);
    Event UpdateEvent(Event ev);
    void DeleteEvent(int id);
    IEnumerable<Event> FilterEvents(DateTime? datum = null, TimeSpan? vreme = null, string? kategorija = null, string? lokacija = null);
}