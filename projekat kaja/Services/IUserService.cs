using projekat_kaja.Models;

namespace projekat_kaja.Services;

public interface IUserService
{
    IEnumerable<User> GetAllUsers();
    User RegisterUser(User user);
    User GetUserById(int id);
    void UpdateUser(User user);
    void DeleteUser(int id);
    User GetByEmail(string email);
    bool MakeReservation(int eventId, int userId, int brmesta);
    //get reservation
    //bool cancel reservation
    //User ValidateUser(string email, string password);
}