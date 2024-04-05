using Blog.Application.Filters;
using Blog.Application.Response;
using Blog.Domain.Core.Entities;
using MediatR;

namespace Blog.Application.Core.Posts.Queries
{
    public class GetPostsQuery : IRequest<PagedResponse<List<Post>>>
    {
        public GetPostsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
