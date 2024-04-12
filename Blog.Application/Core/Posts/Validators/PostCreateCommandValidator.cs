using Blog.Application.Core.Posts.Commands;
using FluentValidation;

namespace Blog.Application.Core.Posts.Validators
{
    public class PostCreateCommandValidator : AbstractValidator<PostCreateCommand>
    {
        private decimal FileSizeMB { get;  set; } = 4 * 1024 * 1024;

        public PostCreateCommandValidator()
        {
            RuleFor(post => post.CategoryId)
                   .NotEmpty().WithMessage("CategoryId is required")
                   .NotNull().WithMessage("CategoryId is required");

            RuleFor(post => post.Title)
                   .NotEmpty().WithMessage("Title is required")
                   .MinimumLength(3).WithMessage("Title require minimum 3 characters")
                   .MaximumLength(255).WithMessage("Title require maximum 255 characters");

            RuleFor(post => post.GetUserId())
                   .NotEmpty().NotNull()
                   .WithMessage("UserId is required");

            RuleFor(post => post.Content)
                .MaximumLength(500).WithMessage("Content require max 500 characters")
                .MinimumLength(3).WithMessage("Content minimum 3 characters")
                .NotEmpty().NotNull().WithMessage("Content is required");

            RuleFor(post => post.Image).NotNull().WithMessage("Image is required");

            RuleFor(post => post.Image)
 
                .Must(x => x.ContentType.Equals("image/jpeg") ||
                           x.ContentType.Equals("image/jpg") ||
                           x.ContentType.Equals("image/png")).When(x => x.Image != null).WithMessage("Image content type is invalid")
                .Must(x => x.Length <= FileSizeMB).When(x => x.Image != null).WithMessage($"Image max size of file is { FileSizeMB / (1024 * 1024)} MB");
                

        }

    }
}
