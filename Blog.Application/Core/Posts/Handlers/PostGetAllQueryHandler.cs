using Blog.Application.Core.Posts.Queries;
using Blog.Domain.Core.Entities;
using Blog.Application.Core.Repositories;
using MediatR;
using Blog.Application.Response;
using Blog.Application.Utils;
using Microsoft.AspNetCore.Http;

namespace Blog.Application.Core.Posts.Handlers
{
    public class PostGetAllQueryHandler : IRequestHandler<GetPostsQuery, PagedResponse<List<Post>>>
    {
        private readonly IPostRepository _postRepository;
        private readonly IUriService _uriService;
        private IHttpContextAccessor _contextAccessor;

        public PostGetAllQueryHandler(IPostRepository postRepository, IUriService uriService, IHttpContextAccessor contextAccessor)
        {
            _postRepository = postRepository;
            _uriService = uriService;
            _contextAccessor = contextAccessor;
        }

        public async Task<PagedResponse<List<Post>>> Handle(GetPostsQuery request, CancellationToken cancellationToken)
        {

            PagedResponse<List<Post>> paginated;

            try
            {

                string route = _contextAccessor.HttpContext.Request.Path;

                int totalPosts = await _postRepository.CountAsync();

                if (totalPosts <= 0) return paginated = new PagedResponse<List<Post>>
                {
                    StatusCode = 404,
                    IsValid = false,
                    Message = "Resource not found",
                };

                var postsPaged = await _postRepository.GetAllPaged(pageSize: request.PageSize, pageNumber: request.PageNumber);

                paginated = PaginationBuilder
                           .CreatePagedReponse(
                              postsPaged,
                              new Filters.PaginationFilter(request.PageNumber, request.PageSize),
                              totalPosts,
                              _uriService,
                              route
                            );

                paginated.StatusCode = 200;
                paginated.IsValid = true;

                return paginated;
            }
            catch (Exception ex)
            {
                return paginated = new PagedResponse<List<Post>>
                {
                    StatusCode = 500,
                    IsValid = false,
                    Message = ex.Message
                };

            }

        }
    }
}
