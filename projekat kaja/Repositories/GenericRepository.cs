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

    public void Delete(int id)
    {
        var obrisi = Context.Find<T>(id);
        if (obrisi != null)
        {
            Context.Remove(obrisi);
        }
    }

    public virtual T Get(int id)
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

    public IQueryable<T> GetQueryable()
    {
        return Context.Set<T>();
    }

    /* public T Get(int id1, int id2)
    {
        throw new NotImplementedException();
    } */
}