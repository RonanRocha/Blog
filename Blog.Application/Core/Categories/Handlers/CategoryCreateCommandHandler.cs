using Blog.Application.Core.Categories.Commands;
using Blog.Domain.Core.Repositories;
using Blog.Domain.Core.Entities;
using MediatR;
using Blog.Application.Response;

namespace Blog.Application.Core.Categories.Handlers
{
    public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, ResponseBase>
    {

        private ICategoryRepository _categoryRepository;

        public CategoryCreateCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }


        public async Task<ResponseBase> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
        {

            ResponseBase response;

            try
            {
                Category category = new Category(request.Name);

                await _categoryRepository.Save(category);

                return response = new ResponseBase
                {
                    IsValid = true,
                    StatusCode = 201,
                    Message = "Category created successfuly"
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
