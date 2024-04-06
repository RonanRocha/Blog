using AutoMapper;
using Blog.Application.Core.Comments.Queries;
using Blog.Application.Core.Comments.Commands;
using Blog.Application.Core.Comments.Response;
using Blog.Application.Core.Services.Interfaces;
using Blog.Application.Core.ViewModels;
using Blog.Domain.Core.Entities;
using MediatR;
using Blog.Application.Filters;
using Blog.Application.Response;

namespace Blog.Application.Core.Services
{
    public class CommentService : ICommentService
    {

        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CommentService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<CommentCommandResponse> AddAsync(CommentCreateCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<CommentCommandResponse> UpdateAsync(CommentUpdateCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<CommentCommandResponse> RemoveAsync(CommentRemoveCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<PagedResponse<List<CommentViewModel>>> GetCommentsByPostIdAsync(int postId, PaginationFilter filter)
        {
            PaginationFilter validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            PagedResponse<List<Comment>> comments = await _mediator.Send(new GetCommentByPostIdQuery(postId, validFilter.PageNumber, validFilter.PageSize));
            return _mapper.Map<PagedResponse<List<CommentViewModel>>>(comments);
        }

    }
}
