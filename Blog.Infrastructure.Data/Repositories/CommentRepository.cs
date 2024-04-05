using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using Blog.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Data.Repositories
{
    public class CommentRepository : Repository<Comment, int>, ICommentRepository
    {
        public CommentRepository(BlogDbContext ctx) : base(ctx)
        {
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostAndUserAsync(int postId, string userId)
        {
            return await _ctx.Comments
                             .Where(x => x.PostId == postId && 
                                    x.UserId == userId)
                             .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            return await _ctx.Comments
                             .Where(x => x.PostId == postId)
                             .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(string userId)
        {
            return await _ctx.Comments
                             .Where(x => x.UserId == userId)
                             .ToListAsync();
        }

        public async Task RemoveAsync(Comment comment)
        {
           await _ctx.Comments.Where(x => x.Id == comment.Id).ExecuteDeleteAsync();
        }
    }
}
