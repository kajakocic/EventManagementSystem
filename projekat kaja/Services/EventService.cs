using projekat_kaja.DTOs;
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
    public Event AddEvent(EventDTO eventDTO)
    {
        var postojeciEv = _unitOfWork.EventRepository.Find(e => e.Naziv == eventDTO.Naziv);

        if (postojeciEv.Any())
        {
            throw new InvalidOperationException($"Event: {eventDTO.Naziv} već postoji.");
        }

        var lokacija = _unitOfWork.LokacijaRepository.GetQueryable().FirstOrDefault(l => l.Naziv == eventDTO.Lokacija.Naziv);

        if (lokacija == null)
        {
            throw new InvalidOperationException("Prosledjena lokacija ne postoji.");
        }

        var kategorija = _unitOfWork.KategorijaRepositoriy.GetQueryable().FirstOrDefault(k => k.Naziv == eventDTO.Kategorija.Naziv);

        if (kategorija == null)
        {
            throw new InvalidOperationException("Prosledjena kategorija ne postoji.");
        }

        var noviEv = new Event
        {
            Naziv = eventDTO.Naziv,
            Datum = eventDTO.Datum,
            Kapacitet = eventDTO.Kapacitet,
            Opis = eventDTO.Opis,
            CenaKarte = eventDTO.CenaKarte,
            URLimg = eventDTO.URLimg,
            KategorijaEvent = kategorija,
            LokacijaEvent = lokacija
        };

        _unitOfWork.EventRepository.Add(noviEv);
        _unitOfWork.SaveChanges();

        return noviEv;
    }

    public IEnumerable<Event> GetAllEvents()
    {
        return _unitOfWork.EventRepository.GetAll();
    }

    public Event GetEventById(int id)
    {
        if (id <= 0)
        {
            throw new InvalidOperationException("Neispravan id.");
        }

        var ne_postoji = _unitOfWork.EventRepository.Find(x => x.ID == id);

        if (!ne_postoji.Any())
        {
            throw new InvalidOperationException($"Event sa id: {id} ne postoji.");
        }

        return _unitOfWork.EventRepository.Get(id);
    }

    public void DeleteEvent(int id)
    {
        if (id <= 0)
        {
            throw new InvalidOperationException("Neispravan id.");
        }

        var ne_postoji = _unitOfWork.EventRepository.Find(x => x.ID == id);

        if (!ne_postoji.Any())
        {
            throw new InvalidOperationException($"Event sa id: {id} ne postoji.");
        }
        _unitOfWork.EventRepository.Delete(id);
        _unitOfWork.SaveChanges();
    }

    public IEnumerable<Event> FilterEvents(DateTime? datum = null, string? kategorija = null, string? lokacija = null)
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