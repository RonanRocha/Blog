using Blog.Domain.Core.Entities;
using FluentValidation;

namespace Blog.Domain.Core.Validation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().NotNull()
                .WithMessage("Name is required")
                .MinimumLength(3).WithMessage("Name require minimum 3 characters")
                .MaximumLength(255).WithMessage("Name require maximum 255 characters");

        }
    }
}
