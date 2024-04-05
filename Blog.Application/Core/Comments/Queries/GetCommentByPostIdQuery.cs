using Blog.Domain.Core.Entities;
using MediatR;

namespace Blog.Application.Core.Comments.Queries
{
    public class GetCommentByPostIdQuery : IRequest<IEnumerable<Comment>>
    {
        public GetCommentByPostIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
