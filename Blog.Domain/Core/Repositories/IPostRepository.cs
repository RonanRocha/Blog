using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;

namespace Blog.Application.Core.Repositories
{
    public interface IPostRepository : IRepository<Post, int>
    {
        Task RemoveAsync(Post post);
        Task<List<Post>> GetAllPaged(int pageNumber, int pageSize);

      
    }
}
