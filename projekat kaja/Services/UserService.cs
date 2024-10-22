using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity;
using projekat_kaja.Models;
using projekat_kaja.Repositories;

namespace projekat_kaja.Services;

public class UserService : IUserService
{
    private readonly IUserRepository UserRepository;
    private readonly string PasswordHashKey = "key";

    public UserService(IUserRepository userRepository)
    {
        UserRepository = userRepository;
    }
    public void DeleteUser(int id)
    {
        UserRepository.Delete(id);
        UserRepository.SaveChanges();
    }

    public User GetUserById(int id)
    {
        return UserRepository.Get(id);
    }

    public User RegisterUser(User user)
    {
        var salt = GenerateSalt();
        //user.Password = HashPassword(user.Password, PasswordHashKey);

        UserRepository.Add(user);
        UserRepository.SaveChanges();
        return user;
    }

    public void UpdateUser(User user)
    {
        UserRepository.Update(user);
        UserRepository.SaveChanges();
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
    private byte[] GenerateSalt()
    {
        byte[] salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private string HashPassword(string password, byte[] salt)
    {
        var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 32));
        return hashed;
    }

    private bool VerifyPassword(string password, string storedHash, string storedSalt)
    {
        var saltBytes = Convert.FromBase64String(storedSalt);
        var hashedInputPassword = HashPassword(password, saltBytes);
        return hashedInputPassword == storedHash;
    }
}