using projekat_kaja.Models;

namespace projekat_kaja.Services;

public interface IReviewService 
{
    Review GetReview(int userId, int eventId);
}