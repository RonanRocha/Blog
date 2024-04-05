using Blog.Application.Core.Posts.Queries;
using Blog.Domain.Core.Entities;
using Blog.Application.Core.Repositories;
using MediatR;

namespace Blog.Application.Core.Posts.Handlers
{
    public class PostGetByIdQueryHandler : IRequestHandler<GetPostByIdQuery, Post>
    {
        private readonly IPostRepository _postRepository;

        public PostGetByIdQueryHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<Post> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            return await _postRepository.GetById(request.Id);
        }
    }
}
