using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public class LocationRepository : GenericRepository<Location>, ILocationRepository
{
    public LocationRepository(EMSContext context) : base(context)
    {
    }
}