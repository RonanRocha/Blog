namespace Blog.Domain.Core.Repositories
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        Task<IList<TEntity>> GetAll();
        Task Save(TEntity entity);
        Task<TEntity> GetById(TKey id);
        Task Update(TEntity entity);

    }
}
