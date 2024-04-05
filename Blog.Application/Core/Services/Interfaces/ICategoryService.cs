using Blog.Application.Core.Categories.Commands;
using Blog.Application.Core.Categories.Response;
using Blog.Application.Core.ViewModels;

namespace Blog.Application.Core.Services.Interfaces
{
    public interface ICategoryService
    {
   
        Task<CategoryCommandResponse> AddAsync(CategoryCreateCommand command);
        Task<CategoryCommandResponse> UpdateAsync(CategoryUpdateCommand command);
        Task<CategoryCommandResponse> RemoveAsync(CategoryRemoveCommand command);
        Task<IEnumerable<CategoryViewModel>> GetAll();
        Task<CategoryViewModel> GetByIdAsync(int id);   
    }
}
