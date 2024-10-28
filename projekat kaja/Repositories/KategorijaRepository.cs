using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public class KategorijaRepository : GenericRepository<Kategorija>, IKategorijaRepositoriy
{
    public KategorijaRepository(EMSContext context) : base(context)
    {
    }
}