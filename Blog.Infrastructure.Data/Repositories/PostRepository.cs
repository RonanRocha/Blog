using Blog.Domain.Core.Entities;
using Blog.Application.Core.Repositories;
using Blog.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Repositories
{
    public class PostRepository : Repository<Post, int>, IPostRepository
    {
        public PostRepository(BlogDbContext ctx) : base(ctx)
        {

        }

        public async Task<int> CountAsync()
        {
           return  await _ctx.Posts.Select(p => p.Id).CountAsync();
        }

        public async Task<List<Post>> GetAllPaged(int pageNumber, int pageSize)
        {
            return await _ctx.Posts.Include(x => x.User)
                                     .Include(x => x.Category)
                                     .Skip((pageNumber - 1) * pageSize)
                                     .Take(pageSize)
                                     .ToListAsync();
        }

        public async Task RemoveAsync(Post post)
        {
            var postDatabase = await _ctx.Posts
                                         .Include(x => x.Comments)
                                         .Where(x => x.Id == post.Id)
                                         .FirstOrDefaultAsync();

            _ctx.Posts.Remove(postDatabase);
            await _ctx.SaveChangesAsync();
        }
    }
}
