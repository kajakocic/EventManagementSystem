using projekat_kaja.Models;

namespace projekat_kaja.Services;

public interface IUserService
{
    User RegisterUser(User user);
    User GetUserById(int id);
    void UpdateUser(User user);
    void DeleteUser(int id);
    //User ValidateUser(string email, string password);
}