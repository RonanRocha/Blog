using AutoMapper;
using Blog.Application.Core.Posts.Commands;
using Blog.Application.Core.Posts.Response;
using Blog.Application.Core.Services.Interfaces;
using Blog.Application.Core.ViewModels;
using Blog.Application.Core.Posts.Queries;
using Blog.Domain.Core.Entities;
using MediatR;
using System.Security.Claims;
using Blog.Application.Filters;
using Blog.Application.Response;

namespace Blog.Application.Core.Services
{
    public class PostService : IPostService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public PostService(IMediator mediator, IMapper mapper)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<PostCommandResponse> AddAsync(PostCreateCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<PostViewModel> GetByIdAsync(int id)
        {
            Post post = await _mediator.Send(new GetPostByIdQuery(id));
            return _mapper.Map<PostViewModel>(post);
        }

        public async Task<PagedResponse<List<PostViewModel>>> GetPostsAsync(PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var posts = await _mediator.Send(new GetPostsQuery(validFilter.PageNumber, validFilter.PageSize));
            return _mapper.Map<PagedResponse<List<PostViewModel>>>(posts);

        }

        public async Task<PostCommandResponse> RemoveAsync(PostRemoveCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        public async Task<PostCommandResponse> UpdateAsync(PostUpdateCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
