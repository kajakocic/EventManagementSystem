using projekat_kaja.Models;
using projekat_kaja.UnitOfWork;

namespace projekat_kaja.Services;

public class KategorijaService : IKategorijaService
{
    private readonly IUnitOfWOrk _unitOfWork;
    public KategorijaService(IUnitOfWOrk unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public Kategorija AddKat(Kategorija kat)
    {
        if (kat.Naziv == "" || kat == null)
        {
            throw new InvalidOperationException("Unesi kategoriju.");
        }

        var postojecaKat = _unitOfWork.KategorijaRepositoriy.Find(k => k.Naziv == kat.Naziv);

        if (postojecaKat.Any())
        {
            throw new InvalidOperationException("Kategorija sa istim nazivom već postoji.");
        }

        _unitOfWork.KategorijaRepositoriy.Add(kat);
        _unitOfWork.SaveChanges();

        return kat;
    }

    public IEnumerable<Kategorija> GetAllKat()
    {
        return _unitOfWork.KategorijaRepositoriy.GetAll();
    }
    public void DeleteKat(int id)
    {
        if (id <= 0)
        {
            throw new InvalidOperationException("Neispravan id.");
        }

        var ne_postoji = _unitOfWork.KategorijaRepositoriy.Find(x => x.ID == id);

        if (!ne_postoji.Any())
        {
            throw new InvalidOperationException($"Kategorija sa id: {id} ne postoji.");
        }

        _unitOfWork.KategorijaRepositoriy.Delete(id);
        _unitOfWork.SaveChanges();
    }
}