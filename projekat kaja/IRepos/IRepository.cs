using System.Linq.Expressions;

namespace projekat_kaja.Repositories;

public interface IRepository<T> where T : class
{
    T Add(T entity);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
    T Get(int id);
    T Update(T x);
    void Delete(int id);
    //T Get(int id1, int id2);
    IQueryable<T> GetQueryable();
    void SaveChanges();
}