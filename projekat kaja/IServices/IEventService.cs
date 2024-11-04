using projekat_kaja.DTOs;
using projekat_kaja.Models;

namespace projekat_kaja.Services;
public interface IEventService
{
    Event AddEvent(EventDTO eventDTO);
    IEnumerable<EventDTO> GetAllEvents();
    Event GetEventById(int id);
    //IEnumerable<ReviewDTO> GetReviews(int eventid);
    Event UpdateEvent(Event ev);
    void DeleteEvent(int id);
    IEnumerable<Event> FilterEvents(DateTime? datum = null, string? kategorija = null, string? lokacija = null);
}