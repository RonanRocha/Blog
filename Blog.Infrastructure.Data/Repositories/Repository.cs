using Blog.Domain.Core.Repositories;
using Blog.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Repositories
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {

        protected BlogDbContext _ctx;

        public Repository(BlogDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IList<TEntity>> GetAll()
        {
            return await _ctx.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(TKey id)
        {
            return await _ctx.Set<TEntity>().FindAsync(id);
        }

        public async Task Save(TEntity entity)
        {
            await _ctx.Set<TEntity>().AddAsync(entity);
            await _ctx.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            _ctx.Set<TEntity>().Update(entity);
            await _ctx.SaveChangesAsync();

        }
    
    }
}
