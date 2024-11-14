using projekat_kaja.DTOs;
using projekat_kaja.Models;

namespace projekat_kaja.Services;
public interface IEventService
{
    Event AddEvent(AddEventDTO addEventDTO);
    IEnumerable<EventDTO> GetAllEvents();
    Event GetEventById(int id);
    //IEnumerable<ReviewDTO> GetReviews(int eventid);
    Event UpdateEvent(AddEventDTO eventDTO);
    void DeleteEvent(int id);
    IEnumerable<Event> FilterEvents(DateTime? datum = null, string? kategorija = null, string? lokacija = null);
}