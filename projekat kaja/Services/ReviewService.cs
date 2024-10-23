using projekat_kaja.Models;
using projekat_kaja.UnitOfWork;

namespace projekat_kaja.Services;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWOrk UnitOfWork;
    public ReviewService(IUnitOfWOrk unitOfWOrk)
    {
        UnitOfWork = unitOfWOrk;
    }
    public Review GetReview(int userId, int eventId)
    {
        throw new NotImplementedException();
        //return UnitOfWork.ReviewRepository.FirstOrDefault(r => r.UserReview == userId && r.EventReview == eventId);
    }
}