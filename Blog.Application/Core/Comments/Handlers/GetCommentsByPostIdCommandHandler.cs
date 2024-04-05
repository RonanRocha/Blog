using Blog.Application.Core.Comments.Queries;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using MediatR;

namespace Blog.Application.Core.Comments.Handlers
{
    public class GetCommentsByPostIdCommandHandler : IRequestHandler<GetCommentByPostIdQuery, IEnumerable<Comment>>
    {

        private readonly ICommentRepository _commentRepository;

        public GetCommentsByPostIdCommandHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async Task<IEnumerable<Comment>> Handle(GetCommentByPostIdQuery request, CancellationToken cancellationToken)
        {
            return await _commentRepository.GetCommentsByPostIdAsync(request.Id);
        }
    }
}
