using System.Linq.Expressions;
using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public abstract class GenericRepository<T> : Irepository<T> where T : class
{
    protected EMSContext Context;

    protected GenericRepository(EMSContext context)
    {
        Context = context;
    }
    public T Add(T entitet) 
    {
        return Context.Add(entitet).Entity;
    }

    /* public Task Delete(int id)
    {
        throw new NotImplementedException();
    }
    */
    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> p)
    {
        return Context.Set<T>()
            .AsQueryable()
            .Where(p)
            .ToList();
    }

    public virtual IEnumerable<T> GetAll()
    {
        return Context.Set<T>().ToList();
    }

    public virtual T GetByID(int id)
    {
        return Context.Find<T>(id);  //sta ovde?
    }

    public virtual void SaveChanges()
    {
        Context.SaveChanges();
    }

    public virtual T Update(T entitet)
    {
        return Context.Update(entitet).Entity;
    }
}
