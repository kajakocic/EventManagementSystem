using projekat_kaja.Models;
using projekat_kaja.Repositories;

namespace projekat_kaja.UnitOfWork;

public interface IUnitOfWOrk
{
    IEventRepositoriy EventRepository { get; }
    IUserRepository UserRepository { get; }
    IReviewRepository ReviewRepository { get; }
    IKategorijaRepositoriy KategorijaRepositoriy { get; }
    ILocationRepository LokacijaRepository { get; }

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

    private IReviewRepository reviewRepository;
    public IReviewRepository ReviewRepository
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

    private IKategorijaRepositoriy kategorijaRepository;
    public IKategorijaRepositoriy KategorijaRepositoriy
    {
        get
        {
            if (kategorijaRepository == null)
            {
                kategorijaRepository = new KategorijaRepository(Context);
            }
            return kategorijaRepository;
        }
    }

    private ILocationRepository lokacijaRepository;
    ILocationRepository IUnitOfWOrk.LokacijaRepository
    {
        get
        {
            if (lokacijaRepository == null)
            {
                lokacijaRepository = new LocationRepository(Context);
            }
            return lokacijaRepository;
        }
    }

    public void SaveChanges()
    {
        Context.SaveChanges();
    }
}