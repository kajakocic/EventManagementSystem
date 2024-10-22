using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(EMSContext context) : base(context)
    {
    }

    public User GetByUsername(string email)
    {
        return Context.Users.SingleOrDefault(u => u.Email == email);
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
}