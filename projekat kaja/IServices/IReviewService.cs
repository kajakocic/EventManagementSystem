using projekat_kaja.DTOs;
using projekat_kaja.Models;

namespace projekat_kaja.Services;

public interface IReviewService
{
    /* Review GetReview(int userId, int eventId);
    Review AddReview(ReviewDTO review);
    void DeleteReview(int userId, int eventId); */

    Review AddReview(ReviewDTO reviewDTO);
    IEnumerable<ReviewDTO> GetReview(int eventId);
}