using Blog.Application.Core.Categories.Commands;
using FluentValidation;

namespace Blog.Application.Core.Categories.Validators
{
    public class CategoryCreateCommandValidator : AbstractValidator<CategoryCreateCommand>
    {
        public CategoryCreateCommandValidator()
        {
            RuleFor(x => x.Name)
             .NotEmpty().NotNull()
             .WithMessage("Name is required")
             .MinimumLength(3).WithMessage("Name require minimum 3 characters")
             .MaximumLength(255).WithMessage("Name require maximum 255 characters");
        }
    }
}
