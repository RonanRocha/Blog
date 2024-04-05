using Blog.Application.Core.Comments.Commands;
using Blog.Application.Core.Comments.Response;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Blog.Application.Core.Comments.Handlers
{
    public class CommentCreateCommandHandler : IRequestHandler<CommentCreateCommand, CommentCommandResponse>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IValidator<Comment> _validator;


        public CommentCreateCommandHandler(ICommentRepository commentRepository, IValidator<Comment> validator)
        {
            _validator = validator;
            _commentRepository = commentRepository;
        }

        public async Task<CommentCommandResponse> Handle(CommentCreateCommand request, CancellationToken cancellationToken)
        {
            CommentCommandResponse response;

            try
            {

                Comment comment = new Comment(userId: request.UserId, postId: request.PostId, message: request.Message);

                ValidationResult vr = await _validator.ValidateAsync(comment);

                if (!vr.IsValid) return response = new CommentCommandResponse
                {
                    IsValid = vr.IsValid,
                    StatusCode = 400,
                    Message = "Validation error",
                    Errors = vr.ToDictionary()
                };

                await _commentRepository.Save(comment);

                return response = new CommentCommandResponse
                {

                    IsValid = vr.IsValid,
                    Message = "Comment created successfuly",
                    StatusCode = 201,

                };

            }
            catch (Exception ex)
            {
                return response = new CommentCommandResponse
                {
                    IsValid = false,
                    StatusCode = 500,
                    Message = ex.Message,

                };

            }


        }
    }
}
