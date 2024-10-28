using projekat_kaja.Models;
using projekat_kaja.UnitOfWork;

namespace projekat_kaja.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWOrk _unitOfWork;
    //private readonly string PasswordHashKey = "key";

    public UserService(IUnitOfWOrk unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void DeleteUser(int id)
    {
        _unitOfWork.UserRepository.Delete(id);
        _unitOfWork.UserRepository.SaveChanges();
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _unitOfWork.UserRepository.GetAll();
    }

    public User GetByEmail(string email)
    {
        var korisnici = _unitOfWork.UserRepository.GetQueryable();
        return korisnici.FirstOrDefault(u => u.Email == email);
    }

    public User GetUserById(int id)
    {
        return _unitOfWork.UserRepository.Get(id);
    }
    public List<RegistrationDTO> GetUserRegistrations(int userid)
    {
        var korisnik = _unitOfWork.UserRepository.GetUserWhithEvent(userid);
        if (korisnik == null)
        {
            throw new ArgumentException($"Korisnik ID: {userid} nije pronadjen.");
        }
        var registracije = korisnik.EventsUsers
            .Select(ue => new RegistrationDTO
            {
                EventId = ue.EventUser.ID,
                EventName = ue.EventUser.Naziv,
                EventDate = ue.EventUser.Datum,
                EventLocation = ue.EventUser.LokacijaEvent.Naziv
            }).ToList();

        return registracije;
    }

    public bool MakeReservation(int eventId, int userId, int brmesta)
    {
        var korisnik = _unitOfWork.UserRepository.Get(userId);
        if (korisnik == null)
        {
            throw new ApplicationException($"Korisnik ID: {userId} nije pronadjen.");
        }
        var dogadjaj = _unitOfWork.EventRepository.Get(eventId);
        if (dogadjaj == null)
        {
            throw new ApplicationException($"Event ID: {userId} nije pronadjen.");
        }
        if (dogadjaj.Kapacitet < brmesta || brmesta <= 0)
        {
            throw new ApplicationException("Nema dovoljno raspoloživih mesta.");
        }
        var postojecaRezervacija = korisnik.EventsUsers.FirstOrDefault(e => e.ID == eventId);
        if (postojecaRezervacija != null)
        {
            //UPITNO
            dogadjaj.Kapacitet -= brmesta;
        }
        else
        {
            //UPITNO
            dogadjaj.Kapacitet -= brmesta;
            korisnik.EventsUsers.Add(new Registration
            {
                //UsersEvents = userId,
                //EventsUsers = eventId,
            });
        }
        _unitOfWork.SaveChanges();
        return true;
    }

    public User RegisterUser(User user)
    {
        _unitOfWork.UserRepository.Add(user);
        _unitOfWork.UserRepository.SaveChanges();
        return user;
    }

    public void UpdateUser(User user)
    {
        _unitOfWork.UserRepository.Update(user);
        _unitOfWork.UserRepository.SaveChanges();
    }

    /* public User ValidateUser(string email, string password)
    {
        var user = UserRepository.GetByUsername(email);
        if (user != null && VerifyPassword(password, user.Password, user.Salt))
        {
            return user;
        }
        return null;
    } */
}