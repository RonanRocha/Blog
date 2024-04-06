using Blog.Application.Core.Comments.Queries;
using Blog.Application.Response;
using Blog.Application.Utils;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Blog.Application.Core.Comments.Handlers
{
    public class GetCommentsByPostIdCommandHandler : IRequestHandler<GetCommentByPostIdQuery, PagedResponse<List<Comment>>>
    {

        private readonly ICommentRepository _commentRepository;
        private readonly IUriService _uriService;
        private IHttpContextAccessor _contextAccessor;

        public GetCommentsByPostIdCommandHandler(ICommentRepository commentRepository, IUriService uriService, IHttpContextAccessor contextAccessor)
        {
            _commentRepository = commentRepository;
            _uriService = uriService;   
            _contextAccessor = contextAccessor;
        }

        public async Task<PagedResponse<List<Comment>>> Handle(GetCommentByPostIdQuery request, CancellationToken cancellationToken)
        {
            PagedResponse<List<Comment>> pagedResponse;

            try
            {
                string route = _contextAccessor.HttpContext.Request.Path;

                int totalComments = await _commentRepository.CountByPostAsync(request.PostId);

                if (totalComments <= 0) return pagedResponse = new PagedResponse<List<Comment>>
                {
                    StatusCode = 404,
                    IsValid = false,
                    Message = "Resource not found"
                };

                var commentsPaged = await _commentRepository.GetCommentsByPostIdAsync(request.PostId,request.PageNumber, request.PageSize);

                pagedResponse = PaginationBuilder.CreatePagedReponse(
                    commentsPaged,
                    new Filters.PaginationFilter(1,100),
                    totalComments,
                    _uriService,
                    route
                );

                pagedResponse.StatusCode = 200;
                pagedResponse.IsValid = true;

                return pagedResponse;
            }
            catch (Exception ex)
            {
                return pagedResponse = new PagedResponse<List<Comment>>
                {
                    IsValid = false,
                    StatusCode = 500,
                    Message = ex.Message,
                };
            }
        }
    }
}
