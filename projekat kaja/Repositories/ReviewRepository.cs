using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public class ReviewRepository : GenericRepository<Review>
{
    public ReviewRepository(EMSContext context) : base(context)
    {
    }
}