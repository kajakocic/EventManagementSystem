using System.Threading.Tasks.Dataflow;
using Microsoft.EntityFrameworkCore;
using projekat_kaja.DTOs;
using projekat_kaja.Models;
using projekat_kaja.Services;
using projekat_kaja.UnitOfWork;

public class RegistrationService : IRegistrationService
{
    private readonly IUnitOfWOrk _unitOfWork;
    public RegistrationService(IUnitOfWOrk unitOfWOrk)
    {
        _unitOfWork = unitOfWOrk;
    }

    public void DeleteReservation(int id)
    {
        if (id <= 0)
        {
            throw new InvalidOperationException("Neispravan id.");
        }

        var rezervacija = _unitOfWork.RegistrationRepository.GetQueryable().Include(x => x.EventUser)
        .Where(r => r.ID == id).FirstOrDefault();

        if (rezervacija == null)
        {
            throw new InvalidOperationException("Rezervacija ne postoji.");
        }


        var ev = rezervacija.EventUser;

        ev.Kapacitet += rezervacija.BrojMesta;

        _unitOfWork.EventRepository.Update(ev);
        _unitOfWork.RegistrationRepository.Delete(id);
        _unitOfWork.SaveChanges();
    }

    public IEnumerable<RegistrationDTO> GetReservations(int userId)
    {
        if (userId < 0)
        {
            throw new InvalidOperationException("Id usera nije validan.");
        }

        // var reservations = _unitOfWork.RegistrationRepository.GetQueryable()
        // .Include(e => e.EventUser)
        // .Include(u => u.UserEvent)
        // .ToList();

        var reservations = _unitOfWork.RegistrationRepository
                        .GetQueryable().Include(x => x.UserEvent).Include(x => x.EventUser)
                            .Where(x => x.UserEvent.ID == userId).ToList();



        var reservationDtos = reservations.Select(r => new RegistrationDTO
        {
            UserId = r.UserEvent.ID,
            UserName = r.UserEvent.Email,
            EventId = r.EventUser.ID,
            EventName = r.EventUser.Naziv,
            BrMesta = r.BrojMesta

        });

        return reservationDtos;
    }

    public RegistrationDTO GetReservation(int id)
    {
        if (id <= 0)
        {
            throw new InvalidOperationException("Neispravan id.");
        }

        var rezervacija = _unitOfWork.RegistrationRepository
                        .GetQueryable().Include(x => x.UserEvent).Include(x => x.EventUser)
                        .FirstOrDefault(r => r.ID == id);

        if (rezervacija == null)
        {
            throw new InvalidOperationException("Rezervacija ne postoji.");
        }

        var rez = new RegistrationDTO
        {
            UserId = rezervacija.UserEvent.ID,
            UserName = rezervacija.UserEvent.Email,
            EventId = rezervacija.EventUser.ID,
            EventName = rezervacija.EventUser.Naziv,
            BrMesta = rezervacija.BrojMesta
        };

        return rez;
    }

    public Registration MakeReservation(int eventId, int userId, int brMesta)
    {
        var ev = _unitOfWork.EventRepository.Find(e => e.ID == eventId).FirstOrDefault();
        var us = _unitOfWork.UserRepository.Find(u => u.ID == userId).FirstOrDefault();

        if (ev.Kapacitet < brMesta)
        {
            throw new InvalidOperationException("Sva mesta su popunjena.");
        }

        ev.Kapacitet -= brMesta;

        var novaRezervacija = new Registration
        {
            UserEvent = us,
            EventUser = ev,
            BrojMesta = brMesta
        };

        _unitOfWork.EventRepository.Update(ev);
        _unitOfWork.RegistrationRepository.Add(novaRezervacija);
        _unitOfWork.SaveChanges();

        return novaRezervacija;
    }

    public Registration AddReservation(AddRegDTO reservationDto)
    {
        var postojiEvent = _unitOfWork.EventRepository.GetQueryable().FirstOrDefault(e => e.ID == reservationDto.EventId);
        if (postojiEvent == null)
        {
            throw new InvalidOperationException("Prosledjeni event ne postoji.");
        }

        var postojiUser = _unitOfWork.UserRepository.GetQueryable().FirstOrDefault(u => u.ID == reservationDto.UserId);
        if (postojiEvent == null)
        {
            throw new InvalidOperationException("Prosledjeni event ne postoji.");
        }

        if (postojiEvent.Kapacitet < reservationDto.BrMesta)
        {
            throw new InvalidOperationException("Sva mesta su popunjena.");
        }

        postojiEvent.Kapacitet -= reservationDto.BrMesta;

        var novaRezervacija = new Registration
        {
            EventUser = postojiEvent,
            UserEvent = postojiUser,
            BrojMesta = reservationDto.BrMesta
        };

        _unitOfWork.EventRepository.Update(postojiEvent);
        _unitOfWork.RegistrationRepository.Add(novaRezervacija);
        _unitOfWork.SaveChanges();

        return novaRezervacija;
    }
}
