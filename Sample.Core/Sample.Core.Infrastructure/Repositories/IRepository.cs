namespace Sample.Core.Infrastructure.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    
    Task<TEntity?> GetByIdAsync(Guid id);
    
    Task<TEntity> AddAsync(TEntity entity);
    
    Task UpdateAsync(TEntity entity);
    
    Task DeleteAsync(Guid id);
}