using Blog.Application.Core.Categories.Commands;
using Blog.Application.Core.Categories.Response;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;


namespace Blog.Application.Core.Categories.Handlers
{
    public class CategoryUpdateCommandHandler : IRequestHandler<CategoryUpdateCommand, CategoryCommandResponse>
    {
        private readonly IValidator<Category> _validator;
        private readonly ICategoryRepository _categoryRepository;

        public CategoryUpdateCommandHandler(IValidator<Category> validator, ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _validator = validator;
        }

        public async Task<CategoryCommandResponse> Handle(CategoryUpdateCommand request, CancellationToken cancellationToken)
        {
            CategoryCommandResponse response;
            try
            {
                Category category = await _categoryRepository.GetById(request.Id);

                if (category == null) return response = new CategoryCommandResponse
                {
                    StatusCode = 404,
                    Message = "Resource not found",
                    IsValid = false
                };

                ValidationResult vr = await _validator.ValidateAsync(category);

                if (!vr.IsValid) return response = new CategoryCommandResponse
                {
                    IsValid = vr.IsValid,
                    StatusCode = 400,
                    Message = "Validation error",
                    Errors = vr.ToDictionary()
                };

                category.UpdateCategory(request.Name);

                await _categoryRepository.Update(category);

                return response = new CategoryCommandResponse
                {
                    IsValid = true,
                    StatusCode = 204,
                    Message = "Category update successfuly"
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
