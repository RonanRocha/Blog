using Blog.Domain.Core.Entities;

namespace Blog.Domain.Core.Repositories
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task RemoveAsync(Category category);
    }
}
