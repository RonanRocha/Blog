using Blog.Domain.Core.Entities;
using MediatR;

namespace Blog.Application.Core.Categories.Queries
{
    public class GetCategoriesQuery : IRequest<IEnumerable<Category>>
    {
    }
}
