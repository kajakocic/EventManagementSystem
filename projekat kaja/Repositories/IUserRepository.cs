using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public interface IUserRepository : IRepository<User>
{
    User GetByUsername(string email);
}