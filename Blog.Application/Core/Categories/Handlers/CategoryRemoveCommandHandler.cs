using Blog.Application.Core.Categories.Commands;
using Blog.Application.Response;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using MediatR;

namespace Blog.Application.Core.Categories.Handlers
{
    public class CategoryRemoveCommandHandler : IRequestHandler<CategoryRemoveCommand, ResponseBase>
    {
        private ICategoryRepository _categoryRepository;
        public CategoryRemoveCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<ResponseBase> Handle(CategoryRemoveCommand request, CancellationToken cancellationToken)
        {
            ResponseBase response;
            try
            {
                Category category = await _categoryRepository.GetById(request.Id);

                if (category == null) return response = new ResponseBase
                {
                    IsValid = false,
                    StatusCode = 404,
                    Message = "Resource not found"
                };

                await _categoryRepository.RemoveAsync(category);


                return response = new ResponseBase
                {
                    StatusCode = 200,
                    IsValid = true,
                    Message = "Category removed successfuly"
                };


            }
            catch (Exception ex)
            {
                return response = new ResponseBase
                {
                    IsValid = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }

        }
    }
}
