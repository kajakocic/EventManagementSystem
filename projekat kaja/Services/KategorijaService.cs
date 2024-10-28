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

    public Kategorija GetKatById(int id)
    {
        return _unitOfWork.KategorijaRepositoriy.Get(id);
    }
    public void DeleteKat(int id)
    {
         _unitOfWork.KategorijaRepositoriy.Delete(id);
        _unitOfWork.KategorijaRepositoriy.SaveChanges();
    }
}