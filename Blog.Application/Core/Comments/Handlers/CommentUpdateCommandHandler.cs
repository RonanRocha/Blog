using Blog.Application.Core.Comments.Commands;
using Blog.Application.Response;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using MediatR;

namespace Blog.Application.Core.Comments.Handlers
{
    public class CommentUpdateCommandHandler : IRequestHandler<CommentUpdateCommand, ResponseBase>
    {

        private readonly ICommentRepository _commentRepository;

        public CommentUpdateCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<ResponseBase> Handle(CommentUpdateCommand request, CancellationToken cancellationToken)
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

                comment.Update(message: request.Message);


                await _commentRepository.Update(comment);

                return response = new ResponseBase
                {
                    IsValid = true,
                    Message = "Comment updated successfuly",
                    StatusCode = 204
                };

            }
            catch (Exception ex)
            {
                return response = new ResponseBase
                {
                    IsValid = false,
                    Message = ex.Message,
                    StatusCode = 500
                };
            }

        }
    }
}
