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

        public async Task<int> CountByPostAsync(int postId)
        {
            return await _ctx.Comments.Where(x => x.PostId == postId)?.Select(c => c.Id).CountAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostAndUserAsync(int postId, string userId, int pageNumber, int pageSize)
        {
            return await _ctx.Comments
                             .Where(x => x.PostId == postId && x.UserId == userId)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync();
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId, int pageNumber, int pageSize)
        {
            return await _ctx.Comments
                             .Where(x => x.PostId == postId)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(string userId, int pageNumber, int pageSize)
        {
            return await _ctx.Comments
                             .Where(x => x.UserId == userId)
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync();
        }

        public async Task RemoveAsync(Comment comment)
        {
           await _ctx.Comments.Where(x => x.Id == comment.Id).ExecuteDeleteAsync();
        }
    }
}
