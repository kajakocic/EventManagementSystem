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
        return Context.Add(x).Entity;
    }

    public virtual T Update(T x)
    {
        return Context.Update(x).Entity;
    }

    public void Delete(Guid id)
    {
        var deleteX = Context.Find<T>(id);
        Context.Remove(deleteX);
    }

    public virtual T Get(Guid id)
    {
        return Context.Find<T>(id);
    }

    public virtual IEnumerable<T> GetAll()
    {
        return Context.Set<T>().ToList();
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> x)
    {
        return Context.Set<T>().AsQueryable().Where(x).ToList();
    }

    public virtual void SaveChanges()
    {
        Context.SaveChanges();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public T Get(int id)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetQueryable()
    {
        throw new NotImplementedException();
    }
}