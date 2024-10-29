using projekat_kaja.Models;
using projekat_kaja.UnitOfWork;

namespace projekat_kaja.Services;

public class LocationService : ILocationService
{
    private readonly IUnitOfWOrk _unitOfWork;
    public LocationService(IUnitOfWOrk unitOfWOrk)
    {
        _unitOfWork = unitOfWOrk;
    }
    public Location AddLoc(Location loc)
    {
        if (loc.Naziv == "" || loc == null)
        {
            throw new InvalidOperationException("Unesi lokaciju.");
        }

        var postojecaLok = _unitOfWork.LokacijaRepository.Find(l => l.Naziv == loc.Naziv);

        if (postojecaLok.Any())
        {
            throw new InvalidOperationException("Lokacija sa tim nazivom je već uneta.");
        }

        _unitOfWork.LokacijaRepository.Add(loc);
        _unitOfWork.SaveChanges();

        return loc;
    }

    public void DeleteLoc(int id)
    {
        if (id <= 0)
        {
            throw new InvalidOperationException("Neispravan id.");
        }

        var ne_postoji = _unitOfWork.LokacijaRepository.Find(x => x.ID == id);

        if (!ne_postoji.Any())
        {
            throw new InvalidOperationException($"Lokacija sa id: {id} ne postoji.");
        }

        _unitOfWork.LokacijaRepository.Delete(id);
        _unitOfWork.SaveChanges();
    }

    public IEnumerable<Location> GetAllLoc()
    {
        return _unitOfWork.LokacijaRepository.GetAll();
    }

    public Location GetLocById(int id)
    {
        if (id <= 0)
        {
            throw new InvalidOperationException("Neispravan id.");
        }

        var ne_postoji = _unitOfWork.LokacijaRepository.Find(x => x.ID == id);

        if (!ne_postoji.Any())
        {
            throw new InvalidOperationException($"Lokacija sa id: {id} ne postoji.");
        }

        return _unitOfWork.LokacijaRepository.Get(id);
    }
}