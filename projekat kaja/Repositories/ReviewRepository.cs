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
        return Context.Reviews
            .Include(r => r.EventReview)
            .Where(r => r.EventReview.ID == eventid)
            .ToList();
    }
}