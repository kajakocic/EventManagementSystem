using System.Linq.Expressions;

namespace projekat_kaja.Repositories;

public interface IRepository<T> where T : class
{
    T Add(T x);
    T Update(T x);
    void Delete(Guid id);
    T Get(Guid id);
    IEnumerable<T> GetAll();
    IEnumerable<T> Find(Expression<Func<T, bool>> x);
    void SaveChanges();
}