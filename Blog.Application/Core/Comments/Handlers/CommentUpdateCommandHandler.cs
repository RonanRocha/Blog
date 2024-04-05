using Blog.Application.Core.Comments.Commands;
using Blog.Application.Core.Comments.Response;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Blog.Application.Core.Comments.Handlers
{
    public class CommentUpdateCommandHandler : IRequestHandler<CommentUpdateCommand, CommentCommandResponse>
    {

        private readonly ICommentRepository _commentRepository;
        private readonly IValidator<Comment> _validator;

        public CommentUpdateCommandHandler(ICommentRepository commentRepository, IValidator<Comment> validator)
        {
            _commentRepository = commentRepository;
            _validator = validator;
        }
        public async Task<CommentCommandResponse> Handle(CommentUpdateCommand request, CancellationToken cancellationToken)
        {
            CommentCommandResponse response;

            try
            {
                Comment comment = await _commentRepository.GetById(request.Id);

                if (comment == null) return response = new CommentCommandResponse
                {
                    IsValid = false,
                    StatusCode = 404,
                    Message = "Resource not found"
                };

                comment.Update(message: request.Message);

                ValidationResult vr = _validator.Validate(comment);

                if (!vr.IsValid) return response = new CommentCommandResponse
                {
                    IsValid = vr.IsValid,
                    Message = "Validation Error",
                    StatusCode = 400,
                    Errors = vr.ToDictionary()
                };

                await _commentRepository.Update(comment);

                return response = new CommentCommandResponse
                {
                    IsValid = vr.IsValid,
                    Message = "Comment updated successfuly",
                    StatusCode = 204
                };

            }
            catch (Exception ex)
            {
                return response = new CommentCommandResponse
                {
                    IsValid = false,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }

        }
    }
}
