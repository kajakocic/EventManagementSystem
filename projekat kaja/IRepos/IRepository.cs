using System.Linq.Expressions;

namespace projekat_kaja.Repositories;

public interface IRepository<T> where T : class
{
    T Add(T x);
    T Update(T x);
    void Delete(int id);
    T Get(int id);
    //T Get(int id1, int id2);
    IEnumerable<T> GetAll();
    IQueryable<T> GetQueryable();
    IEnumerable<T> Find(Expression<Func<T, bool>> x);
    void SaveChanges();
}