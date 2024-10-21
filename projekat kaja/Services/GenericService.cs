using projekat_kaja.Repositories;

public abstract class GenericService<T> : IService<T> where T : class
{
    protected IRepository<T> Repository;
    protected GenericService(IRepository<T> repository)
    {
        Repository = repository;
    }

    public async Task<T> AddAsync(T x)
    {
        try
        {
            var createX = Repository.Add(x);
            Repository.SaveChanges();
            return await Task.FromResult(createX);
        }
        catch (Exception e)
        {
            throw new Exception("Greška prilikom dodavanja entiteta.", e);
        }
    }

    public async Task<T> UpdateAsync(T x)
    {
        try
        {
            var updateX = Repository.Update(x);
            Repository.SaveChanges();
            return await Task.FromResult(updateX);
        }
        catch (Exception e)
        {
            throw new Exception("Greška prilikom ažuriranja entiteta.", e);
        }
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
        /* _repository.Delete(id);
        _repository.SaveChanges();
        await Task.CompletedTask; */
    }

    public Task<IEnumerable<T>> GetAllAsync()
    {
        throw new NotImplementedException();
        /* return await Task.FromResult(_repository.GetByID(id)); */
    }

    public Task<T> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
        /* return await Task.FromResult(_repository.GetAll()); */
    }

}