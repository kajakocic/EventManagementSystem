using System.Linq.Expressions;
using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public abstract class GenericRepository<T> : IRepository<T> where T : class
{
    protected EMSContext Context;

    public GenericRepository(EMSContext context)
    {
        Context = context;
    }

    public virtual T Add(T x)
    {
        try
        {
            return Context.Add(x).Entity;
        }
        catch (Exception ex)
        {
            throw new Exception("Neispravno dodavanje entiteta.", ex);
        }
    }

    public virtual T Update(T x)
    {
        try
        {
            return Context.Update(x).Entity;
        }
        catch (Exception ex)
        {
            throw new Exception("Neispravno ažuriranje entiteta.", ex);
        }
    }

    public void Delete(Guid id)
    {
        try
        {
            var deleteX = Context.Find<T>(id);
            Context.Remove(deleteX);

        }
        catch (Exception ex)
        {
            throw new Exception("Neispravno brisanje entiteta.", ex);
        }
    }

    public virtual T Get(Guid id)
    {
        try
        {

            return Context.Find<T>(id);
        }
        catch (Exception ex)
        {
            throw new Exception("Neispravno preuzimanje entiteta.", ex);
        }
    }

    public virtual IEnumerable<T> GetAll()
    {
        try
        {
            return Context.Set<T>().ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Neispravno preuzimanje entiteta.", ex);
        }
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> x)
    {
        try
        {
            return Context.Set<T>()
                        .AsQueryable()
                        .Where(x)
                        .ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Neispravno filtriranje entiteta.", ex);
        }
    }

    public virtual void SaveChanges()
    {
        Context.SaveChanges();
    }
}