using Microsoft.EntityFrameworkCore;
using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public class ReviewRepository : GenericRepository<Review>, IReviewRepository
{
    public ReviewRepository(EMSContext context) : base(context)
    {
    }

    public IEnumerable<Review> GetReviewsByEventId(int eventid)
    {
        return _context.Reviews
            .Include(r => r.EventUser)
            .Where(r => r.EventUser.ID == eventid)
            .ToList();
    }
}