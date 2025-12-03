namespace Library.Domain;

public interface IRepository<TEntity>
    where TEntity : class
{
    public Task<TEntity> Create(TEntity entity);

    public Task<TEntity?> Get(Guid entityId);

    public Task<IList<TEntity>> GetAll();

    public Task<TEntity> Update(TEntity entity);

    public Task<bool> Delete(Guid entityId);
}