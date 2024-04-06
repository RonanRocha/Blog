using Blog.Domain.Core.Entities;

namespace Blog.Domain.Core.Repositories
{
    public interface ICommentRepository : IRepository<Comment, int>
    {
        Task<List<Comment>> GetCommentsByPostIdAsync(int postId, int pageNumber, int pageSize);
        Task<IEnumerable<Comment>> GetCommentsByUserIdAsync(string userId, int pageNumber, int pageSize);
        Task<IEnumerable<Comment>> GetCommentsByPostAndUserAsync(int postId, string userId, int pageNumber, int pageSize);
        Task RemoveAsync(Comment comment);
        Task<int> CountByPostAsync(int postId);

    }
}
