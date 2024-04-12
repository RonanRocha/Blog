using Blog.Application.Core.Comments.Commands;
using Blog.Application.Response;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using MediatR;

namespace Blog.Application.Core.Comments.Handlers
{
    public class CommentCreateCommandHandler : IRequestHandler<CommentCreateCommand, ResponseBase>
    {
        private readonly ICommentRepository _commentRepository;



        public CommentCreateCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<ResponseBase> Handle(CommentCreateCommand request, CancellationToken cancellationToken)
        {
            ResponseBase response;

            try
            {

                Comment comment = new Comment(userId: request.UserId, postId: request.PostId, message: request.Message);


                await _commentRepository.Save(comment);

                return response = new ResponseBase
                {

                    IsValid = true,
                    Message = "Comment created successfuly",
                    StatusCode = 201,

                };

            }
            catch (Exception ex)
            {
                return response = new ResponseBase
                {
                    IsValid = false,
                    StatusCode = 500,
                    Message = ex.Message,

                };

            }


        }
    }
}
