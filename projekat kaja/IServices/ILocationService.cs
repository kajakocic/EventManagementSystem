using projekat_kaja.Models;

namespace projekat_kaja.Services;

public interface ILocationService 
{
    Location AddLoc(Location loc);
    IEnumerable<Location> GetAllLoc();
    Location GetLocById(int id);
    void DeleteLoc(int id);
}