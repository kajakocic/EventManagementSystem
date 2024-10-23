using projekat_kaja.Models;
using projekat_kaja.Repositories;

namespace projekat_kaja.UnitOfWork;

public interface IUnitOfWOrk
{
    IRepository<Event> EventRepository { get; }
    IRepository<User> UserRepository { get; }
    IRepository<Review> ReviewRepository { get; }

    void SaveChanges();
}

public class UnitOfWork : IUnitOfWOrk
{
    private EMSContext Context;
    public UnitOfWork(EMSContext context)
    {
        Context = context;
    }

    private IRepository<Event> eventRepository;
    public IRepository<Event> EventRepository
    {
        get
        {
            if (eventRepository == null)
            {
                eventRepository = new EventRepository(Context);
            }
            return eventRepository;
        }
    }

    private IRepository<User> userRepository;
    public IRepository<User> UserRepository
    {
        get
        {
            if (userRepository == null)
            {
                userRepository = new UserRepository(Context);
            }
            return userRepository;
        }
    }

    private IRepository<Review> reviewRepository;
    public IRepository<Review> ReviewRepository
    {
        get
        {
            if (reviewRepository == null)
            {
                reviewRepository = new ReviewRepository(Context);
            }
            return reviewRepository;
        }
    }

    public void SaveChanges()
    {
        Context.SaveChanges();
    }
}