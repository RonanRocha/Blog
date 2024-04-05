using Blog.Domain.Core.Entities;
using MediatR;

namespace Blog.Application.Core.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<Category>
    {
        public GetCategoryByIdQuery(int id)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}
