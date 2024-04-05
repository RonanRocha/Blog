using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using Blog.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Repositories
{
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(BlogDbContext ctx) : base(ctx)
        {
        }

        public async Task RemoveAsync(Category category)
        {
            var categoryDatabase = await _ctx.Categories
                .Include(x => x.Posts)
                .ThenInclude(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == category.Id);

            _ctx.Categories.Remove(categoryDatabase);

           await _ctx.SaveChangesAsync();
        }
    }
}
