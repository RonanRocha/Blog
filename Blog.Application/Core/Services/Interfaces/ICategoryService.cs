using Blog.Application.Core.Categories.Commands;
using Blog.Application.Core.ViewModels;
using Blog.Application.Response;

namespace Blog.Application.Core.Services.Interfaces
{
    public interface ICategoryService
    {
   
        Task<ResponseBase> AddAsync(CategoryCreateCommand command);
        Task<ResponseBase> UpdateAsync(CategoryUpdateCommand command);
        Task<ResponseBase> RemoveAsync(CategoryRemoveCommand command);
        Task<IEnumerable<CategoryViewModel>> GetAll();
        Task<CategoryViewModel> GetByIdAsync(int id);   
    }
}
