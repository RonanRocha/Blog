using Blog.Application.Core.Categories.Commands;
using Blog.Application.Response;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using MediatR;


namespace Blog.Application.Core.Categories.Handlers
{
    public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommand, ResponseBase>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryUpdateCommandHandler( ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<ResponseBase> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            ResponseBase response;
            try
            {
                Category category = await _categoryRepository.GetById(request.Id);

                if (category == null) return response = new ResponseBase
                {
                    StatusCode = 404,
                    Message = "Resource not found",
                    IsValid = false
                };


                category.UpdateCategory(request.Name);

                await _categoryRepository.Update(category);

                return response = new ResponseBase
                {
                    IsValid = true,
                    StatusCode = 204,
                    Message = "Category update successfuly"
                };

            }catch (Exception ex)
            {
                return response = new ResponseBase
                {
                    IsValid = false,
                    StatusCode = 500,
                    Message = ex.Message,
                };
            }
        }
    }
}
