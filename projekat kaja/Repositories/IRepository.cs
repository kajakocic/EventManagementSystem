using System.Linq.Expressions;

namespace projekat_kaja.Repositories;

public interface Irepository<T> where T : class
{
    IEnumerable<T> GetAll();
    T GetByID(int id);
    T Add(T entitet);
    T Update(T entitet);
    //Task Delete(int id);
    IEnumerable<T> Find(Expression<Func<T, bool>> p);
    void SaveChanges();
}