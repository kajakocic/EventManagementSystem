using projekat_kaja.Models;

namespace projekat_kaja.Services;
public interface IKategorijaService
{
    Kategorija AddKat(Kategorija kat);
    IEnumerable<Kategorija> GetAllKat();
    void DeleteKat(int id);
}