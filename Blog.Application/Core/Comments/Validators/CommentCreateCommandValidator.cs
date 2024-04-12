using Blog.Application.Core.Comments.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Core.Comments.Validators
{
    public class CommentCreateCommandValidator : AbstractValidator<CommentCreateCommand>
    {
        public CommentCreateCommandValidator()
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
