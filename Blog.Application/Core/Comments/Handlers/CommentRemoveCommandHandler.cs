using Blog.Application.Core.Comments.Commands;
using Blog.Application.Core.Comments.Response;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using MediatR;

namespace Blog.Application.Core.Comments.Handlers
{
    public class CommentRemoveCommandHandler : IRequestHandler<CommentRemoveCommand, CommentCommandResponse>
    {
        private readonly ICommentRepository _commentRepository;

        public CommentRemoveCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<CommentCommandResponse> Handle(CommentRemoveCommand request, CancellationToken cancellationToken)
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

                await _commentRepository.RemoveAsync(comment);

                return response = new CommentCommandResponse
                {
                    IsValid = true,
                    StatusCode = 200,
                    Message = "Comment removed successfuly"
                };

            }
            catch (Exception ex)
            {
                return response = new CommentCommandResponse
                {
                    IsValid = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }


        }
    }
}
