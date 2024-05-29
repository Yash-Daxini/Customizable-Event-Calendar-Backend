namespace Core.Interfaces.IRepositories;

public interface IRepository<TEntity> where TEntity : class
{
    public Task<int> Add(TEntity entity);
    public Task Update(TEntity entity);
    public Task Delete(TEntity entity);
}
