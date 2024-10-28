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

    public Review AddReview(ReviewDTO review)
    {

        var r = new Review
        {
            UserEvent = new User { ID = review.UserReviewID },
            EventUser = new Event { ID = review.EventReviewID },
            Ocena = review.Ocena,
            Komentar = review.Komentar
        };

        UnitOfWork.ReviewRepository.Add(r);
        UnitOfWork.SaveChanges();
        return r;
    }

    public void DeleteReview(int userId, int eventId)
    {

        var review = GetReview(userId, eventId);
        UnitOfWork.ReviewRepository.Delete(review.ID);
        UnitOfWork.SaveChanges();
    }

    public Review GetReview(int userId, int eventId)
    {
        return UnitOfWork.ReviewRepository
            .Find(r => r.UserEvent.ID == userId && r.EventUser.ID == eventId)
            .FirstOrDefault();
    }
}