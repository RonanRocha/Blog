using Blog.Domain.Core.Entities;
using FluentValidation;

namespace Blog.Domain.Core.Validation
{
    public class CommentValidator : AbstractValidator<Comment>
    {
        public CommentValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty().NotNull()
                .WithMessage("Message is required")
                .MaximumLength(255).WithMessage("Comment permit only 255 characters");

            RuleFor(x => x.UserId).NotEmpty().NotNull().WithMessage("UserId is required");
            RuleFor(x => x.PostId).NotEmpty().NotNull().WithMessage("PostId is required");

        }
    }
}
