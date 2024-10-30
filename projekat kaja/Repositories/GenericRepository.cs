using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using projekat_kaja.Models;

namespace projekat_kaja.Repositories;

public abstract class GenericRepository<T> : IRepository<T> where T : class
{
    protected EMSContext _context;

    public GenericRepository(EMSContext context)
    {
        _context = context;
    }

    public virtual T Add(T entity)
    {
        return _context.Add(entity).Entity;
    }

    public virtual IEnumerable<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
    {
        return _context.Set<T>().AsQueryable().Where(predicate).ToList();
    }

    public virtual T Get(int id)
    {
        return _context.Find<T>(id);
    }

    public virtual T Update(T x)
    {
        return _context.Update(x).Entity;
    }

    public void Delete(int id)
    {
        var obrisi = _context.Find<T>(id);
        if (obrisi != null)
        {
            _context.Remove(obrisi);
        }
    }

    public virtual void SaveChanges()
    {
        _context.SaveChanges();
    }

    public IQueryable<T> GetQueryable()
    {
        return _context.Set<T>().AsTracking();
    }

    /* public T Get(int id1, int id2)
    {
        throw new NotImplementedException();
    } */
}