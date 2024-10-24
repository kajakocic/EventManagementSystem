using Microsoft.EntityFrameworkCore;
using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(EMSContext context) : base(context)
    {
    }

    public override User Update(User x)
    {
        var u = Context.Users.Single(p => p.ID == x.ID);

        u.Ime = x.Ime;
        u.Prezime = x.Prezime;
        u.Password = x.Password;
        u.Email = x.Email;
        u.Tip = x.Tip;

        return base.Update(u);
    }

    public User GetUserWhithEvent(int userid)
    {
        //vrati jednog user-a, ciji id je prosledjen kao param funkcije onoliko puta
        //na koliko se eventa registrovao
        return Context.Users
            .Include(u => u.EventsUsers)
            .ThenInclude(r => r.EventsUsers)
            .FirstOrDefault(u => u.ID == userid);
    }
}