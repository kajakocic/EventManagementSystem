using projekat_kaja.Models;
using projekat_kaja.Repositories;

namespace projekat_kaja.UnitOfWork;

public interface IUnitOfWOrk
{
    IEventRepositoriy EventRepository { get; }
    IUserRepository UserRepository { get; }
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

    private IEventRepositoriy eventRepository;
    public IEventRepositoriy EventRepository
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

    private IUserRepository userRepository;
    public IUserRepository UserRepository
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