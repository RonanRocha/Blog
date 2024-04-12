using Blog.Application.Core.Comments.Commands;
using Blog.Application.Response;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using MediatR;

namespace Blog.Application.Core.Comments.Handlers
{
    public class CommentRemoveCommandHandler : IRequestHandler<CommentRemoveCommand, ResponseBase>
    {
        private readonly ICommentRepository _commentRepository;

        public CommentRemoveCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<ResponseBase> Handle(CommentRemoveCommand request, CancellationToken cancellationToken)
        {
            ResponseBase response;

            try
            {
                Comment comment = await _commentRepository.GetById(request.Id);

                if (comment == null) return response = new ResponseBase
                {
                    IsValid = false,
                    StatusCode = 404,
                    Message = "Resource not found"
                };

                await _commentRepository.RemoveAsync(comment);

                return response = new ResponseBase
                {
                    IsValid = true,
                    StatusCode = 200,
                    Message = "Comment removed successfuly"
                };

            }
            catch (Exception ex)
            {
                return response = new ResponseBase
                {
                    IsValid = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }


        }
    }
}
