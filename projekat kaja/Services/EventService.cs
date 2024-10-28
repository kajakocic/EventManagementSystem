using projekat_kaja.Models;
using projekat_kaja.UnitOfWork;

namespace projekat_kaja.Services;

public class EventService : IEventService
{
    private readonly IUnitOfWOrk _unitOfWork;
    public EventService(IUnitOfWOrk unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public Event AddEvent(Event ev)
    {
        _unitOfWork.EventRepository.Add(ev);
        _unitOfWork.EventRepository.SaveChanges();
        return ev;
    }

    public IEnumerable<Event> GetAllEvents()
    {
        return _unitOfWork.EventRepository.GetAll();
    }

    public Event GetEventById(int id)
    {
        return _unitOfWork.EventRepository.Get(id);
    }

    public void DeleteEvent(int id)
    {
        _unitOfWork.EventRepository.Delete(id);
        _unitOfWork.EventRepository.SaveChanges();
    }

    public IEnumerable<Event> FilterEvents(DateTime? datum = null, TimeSpan? vreme = null, string? kategorija = null, string? lokacija = null)
    {
        var allEvents = _unitOfWork.EventRepository.GetQueryable();

        if (datum.HasValue)
        {
            allEvents = allEvents.Where(e => e.Datum.Date == datum.Value.Date);
        }
        if (!string.IsNullOrWhiteSpace(kategorija))
        {
            allEvents = allEvents.Where(e => e.KategorijaEvent != null && e.KategorijaEvent.Naziv == kategorija);
        }
        if (!string.IsNullOrWhiteSpace(lokacija))
        {
            allEvents = allEvents.Where(e => e.LokacijaEvent != null && e.LokacijaEvent.Naziv == lokacija);
        }

        return allEvents;
    }

    public IEnumerable<ReviewDTO> GetReviews(int eventid)
    {
        var recenzijeEvent = _unitOfWork.ReviewRepository.GetReviewsByEventId(eventid);

        var reviewDtos = recenzijeEvent.Select(r => new ReviewDTO
        {
            UserReviewID = r.UserEvent?.ID ?? 0, // Ako UserReview nije null, uzmi ID, inače 0
            EventReviewID = r.EventUser.ID,
            ImeKorisnika = r.UserEvent?.Ime,
            NazivEventa = r.EventUser.Naziv,
            Ocena = (int)r.Ocena,
            Komentar = r.Komentar
        }).ToList();

        return reviewDtos;
    }

    public Event UpdateEvent(Event ev)
    {
        _unitOfWork.EventRepository.Update(ev);
        _unitOfWork.EventRepository.SaveChanges();
        return ev;
    }
}