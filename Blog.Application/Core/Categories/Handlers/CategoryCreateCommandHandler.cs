using Blog.Application.Core.Categories.Commands;
using Blog.Application.Core.Categories.Response;
using Blog.Domain.Core.Repositories;
using Blog.Domain.Core.Entities;
using FluentValidation;
using MediatR;
using FluentValidation.Results;

namespace Blog.Application.Core.Categories.Handlers
{
    public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, CategoryCommandResponse>
    {

        private ICategoryRepository _categoryRepository;
        private IValidator<Category> _validator;

        public CategoryCreateCommandHandler(ICategoryRepository categoryRepository, IValidator<Category> validator)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }


        public async Task<CategoryCommandResponse> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
        {

            CategoryCommandResponse response;

            try
            {
                Category category = new Category(request.Name);

                ValidationResult vr = await _validator.ValidateAsync(category);

                if (!vr.IsValid) return response = new CategoryCommandResponse
                {
                    IsValid = false,
                    StatusCode = 400,
                    Message = "Validation error",
                    Errors = vr.ToDictionary()
                };

                await _categoryRepository.Save(category);

                return response = new CategoryCommandResponse
                {
                    IsValid = true,
                    StatusCode = 201,
                    Message = "Category created successfuly"
                };

            }catch (Exception ex)
            {
                return response = new CategoryCommandResponse
                {
                    IsValid = false,
                    StatusCode = 500,
                    Message = ex.Message,
                };
            }
           
        }
    }
}
