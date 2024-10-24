using projekat_kaja.Models;
using projekat_kaja.UnitOfWork;

namespace projekat_kaja.Services;

public class EventService : IEventService
{
    private readonly IUnitOfWOrk UnitOfWork;
    public EventService(IUnitOfWOrk unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
    public Event AddEvent(Event ev)
    {
        UnitOfWork.EventRepository.Add(ev);
        UnitOfWork.EventRepository.SaveChanges();
        return ev;
    }

    public void DeleteEvent(int id)
    {
        UnitOfWork.EventRepository.Delete(id);
        UnitOfWork.EventRepository.SaveChanges();
    }

    public IEnumerable<Event> FilterEvents(DateTime? datum = null, TimeSpan? vreme = null, string? kategorija = null, string? lokacija = null)
    {
        var allEvents = UnitOfWork.EventRepository.GetQueryable();

        if (datum.HasValue)
        {
            allEvents = allEvents.Where(e => e.Datum.Date == datum.Value.Date);
        }
        if (vreme.HasValue)
        {
            allEvents = allEvents.Where(e => e.Vreme == vreme.Value);
        }
        if (!string.IsNullOrWhiteSpace(kategorija))
        {
            allEvents = allEvents.Where(e => e.KategorijaEvent != null && e.KategorijaEvent.Naziv == kategorija);
        }
        if (!string.IsNullOrWhiteSpace(lokacija))
        {
            allEvents = allEvents.Where(e => e.LocationEvent != null && e.LocationEvent.Naziv == lokacija);
        }

        return allEvents;
    }

    public IEnumerable<Event> GetAllEvents()
    {
        return UnitOfWork.EventRepository.GetAll();
    }

    public Event GetEventById(int id)
    {
        return UnitOfWork.EventRepository.Get(id);
    }

    public IEnumerable<ReviewDTO> GetReviews(int eventid)
    {
        var recenzijeEvent = UnitOfWork.ReviewRepository.GetReviewsByEventId(eventid);

        var reviewDtos = recenzijeEvent.Select(r => new ReviewDTO
        {
            UserReviewID = r.UserReview?.ID ?? 0, // Ako UserReview nije null, uzmi ID, inače 0
            EventReviewID = r.EventReview.ID,
            ImeKorisnika = r.UserReview?.Ime,
            NazivEventa = r.EventReview.Naziv,
            Ocena = (int)r.Ocena,
            Komentar = r.Komentar
        }).ToList();

        return reviewDtos;
    }

    public Event UpdateEvent(Event ev)
    {
        UnitOfWork.EventRepository.Update(ev);
        UnitOfWork.EventRepository.SaveChanges();
        return ev;
    }
}