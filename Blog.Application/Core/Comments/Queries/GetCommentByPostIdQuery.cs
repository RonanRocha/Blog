using Blog.Application.Response;
using Blog.Domain.Core.Entities;
using MediatR;

namespace Blog.Application.Core.Comments.Queries
{
    public class GetCommentByPostIdQuery : IRequest<PagedResponse<List<Comment>>>
    {
        public GetCommentByPostIdQuery(int postId, int pageNumber, int pageSize)
        {
            PostId = postId;
            PageNumber = pageNumber;
            PageSize = pageSize;

        }

        public int PostId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
