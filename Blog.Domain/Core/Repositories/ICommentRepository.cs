using Blog.Domain.Core.Entities;

namespace Blog.Domain.Core.Repositories
{
    public interface ICommentRepository : IRepository<Comment, int>
    {
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
        Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(string userId);
        Task<IEnumerable<Comment>> GetCommentsByPostAndUserAsync(int postId, string userId);
        Task RemoveAsync(Comment comment);

    }
}
