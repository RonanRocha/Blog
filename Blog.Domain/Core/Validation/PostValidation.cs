using Blog.Domain.Core.Entities;
using FluentValidation;

namespace Blog.Domain.Core.Validation
{
    public class PostValidation : AbstractValidator<Post>
    {
        public PostValidation()
        {
            RuleFor(post => post.CategoryId)
                    .NotEmpty().WithMessage("CategoryId is required")
                    .NotNull().WithMessage("CategoryId is required");

            RuleFor(post => post.Title)
                   .NotEmpty().WithMessage("Title is required")
                   .MinimumLength(3).WithMessage("Title require minimum 3 characters")
                   .MaximumLength(255).WithMessage("Title require maximum 255 characters");

            RuleFor(post => post.UserId)
                   .NotEmpty().NotNull()
                   .WithMessage("UserId is required");

            RuleFor(post => post.Content)
                .MaximumLength(500).WithMessage("Content require max 500 characters")
                .MinimumLength(3).WithMessage("Content minimum 3 characters")
                .NotEmpty().NotNull().WithMessage("Content is required");

            RuleFor(post => post.Image)
                .NotNull().NotEmpty().WithMessage("Image is required")
                .MaximumLength(2083).WithMessage("Image Url max 2083 characters");

            RuleFor(post => post.CreatedAt)
            .NotNull().NotEmpty().WithMessage("Image is required");



        }
    }
}
