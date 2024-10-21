public interface IService<T> where T : class
{
    Task<T> AddAsync(T x);
    Task<T> UpdateAsync(T x);
    Task DeleteAsync(int id);
    Task<T> GetByIdAsync(int id);
    Task<IEnumerable<T>> GetAllAsync();
}