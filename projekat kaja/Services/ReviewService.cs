using Microsoft.EntityFrameworkCore;
using projekat_kaja.DTOs;
using projekat_kaja.Models;
using projekat_kaja.UnitOfWork;

namespace projekat_kaja.Services;

public class ReviewService : IReviewService
{
    private readonly IUnitOfWOrk _unitOfWork;
    public ReviewService(IUnitOfWOrk unitOfWOrk)
    {
        _unitOfWork = unitOfWOrk;
    }

    public Review AddReview(ReviewDTO reviewDTO)
    {
        var reviewExists = _unitOfWork.ReviewRepository.Find(r => r.EventUser.Naziv == reviewDTO.Event && r.UserEvent.Email == reviewDTO.User);
        if (reviewExists.Any())
        {
            throw new InvalidOperationException("Korisnik je već ostavio recenziju.");
        }
        var korisnik = _unitOfWork.UserRepository.GetQueryable().FirstOrDefault(u => u.Email == reviewDTO.User);
        if (korisnik == null)
        {
            throw new InvalidOperationException("Korisnik nije registrovan");
        }
        var dogadjaj = _unitOfWork.EventRepository.GetQueryable().FirstOrDefault(e => e.Naziv == reviewDTO.Event);
        if (dogadjaj == null)
        {
            throw new InvalidOperationException("Događaj za koji želite da ostavite recenziju ne postoji");
        }

        var noviR = new Review
        {
            Ocena = reviewDTO.Ocena,
            Komentar = reviewDTO.Komentar,
            UserEvent = korisnik,
            EventUser = dogadjaj
        };

        _unitOfWork.ReviewRepository.Add(noviR);
        _unitOfWork.SaveChanges();

        return noviR;
    }

    public IEnumerable<ReviewDTO> GetReview(int eventId)
    {
        var reviews = _unitOfWork.ReviewRepository.GetQueryable()
        .Include(e => e.EventUser)
        .Where(e => e.EventUser.ID == eventId)
        .Include(u => u.UserEvent)
        .ToList();

        var reviewDtos = reviews.Select(r => new ReviewDTO
        {
            Id = r.ID,
            Ocena = r.Ocena,
            Komentar = r.Komentar,
            User = r.UserEvent.Email,
            Event = r.EventUser.Naziv
        });

        return reviewDtos;
    }


    /* public Review AddReview(ReviewDTO review)
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
    } */
}