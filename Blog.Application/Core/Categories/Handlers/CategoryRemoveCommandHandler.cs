using Blog.Application.Core.Categories.Commands;
using Blog.Application.Core.Categories.Response;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using MediatR;

namespace Blog.Application.Core.Categories.Handlers
{
    public class CategoryRemoveCommandHandler : IRequestHandler<CategoryRemoveCommand, CategoryCommandResponse>
    {
        private ICategoryRepository _categoryRepository;
        public CategoryRemoveCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<CategoryCommandResponse> Handle(CategoryRemoveCommand request, CancellationToken cancellationToken)
        {
            CategoryCommandResponse response;
            try
            {
                Category category = await _categoryRepository.GetById(request.Id);

                if (category == null) return response = new CategoryCommandResponse
                {
                    IsValid = false,
                    StatusCode = 404,
                    Message = "Resource not found"
                };

                await _categoryRepository.RemoveAsync(category);


                return response = new CategoryCommandResponse
                {
                    StatusCode = 200,
                    IsValid = true,
                    Message = "Category removed successfuly"
                };


            }
            catch (Exception ex)
            {
                return response = new CategoryCommandResponse
                {
                    IsValid = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }

        }
    }
}
