using projekat_kaja.Models;
using projekat_kaja.UnitOfWork;

namespace projekat_kaja.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWOrk UnitOfWork;
    //private readonly string PasswordHashKey = "key";

    public UserService(IUnitOfWOrk unitOfWork)
    {
        UnitOfWork = unitOfWork;
    }
    public void DeleteUser(int id)
    {
        UnitOfWork.UserRepository.Delete(id);
        UnitOfWork.UserRepository.SaveChanges();
    }

    public IEnumerable<User> GetAllUsers()
    {
        return UnitOfWork.UserRepository.GetAll();
    }

    public User GetByEmail(string email)
    {
        var korisnici = UnitOfWork.UserRepository.GetQueryable();
        return korisnici.FirstOrDefault(u => u.Email == email);
    }

    public User GetUserById(int id)
    {
        return UnitOfWork.UserRepository.Get(id);
    }

    public bool MakeReservation(int eventId, int userId, int brmesta)
    {
        var korisnik = UnitOfWork.UserRepository.Get(userId);
        if (korisnik == null)
        {
            throw new ApplicationException($"Korisnik ID: {userId} nije pronadjen.");
        }
        var dogadjaj = UnitOfWork.EventRepository.Get(eventId);
        if (dogadjaj == null)
        {
            throw new ApplicationException($"Event ID: {userId} nije pronadjen.");
        }
        if (dogadjaj.Kapacitet < brmesta || brmesta <= 0)
        {
            throw new ApplicationException("Nema dovoljno raspoloživih mesta.");
        }
        var postojecaRezervacija = korisnik.EventsUsers.FirstOrDefault(e => e.ID == eventId);
        if(postojecaRezervacija!=null)
        {
            //UPITNO
            dogadjaj.Kapacitet-=brmesta;
        }
        else
        {
            //UPITNO
            dogadjaj.Kapacitet-=brmesta;
            korisnik.EventsUsers.Add(new Registration
            {
                UsersEvents = userId,
                EventsUsers = eventId,
            });
        }
        UnitOfWork.SaveChanges();
        return true;
    }

    public User RegisterUser(User user)
    {
        UnitOfWork.UserRepository.Add(user);
        UnitOfWork.UserRepository.SaveChanges();
        return user;
    }

    public void UpdateUser(User user)
    {
        UnitOfWork.UserRepository.Update(user);
        UnitOfWork.UserRepository.SaveChanges();
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