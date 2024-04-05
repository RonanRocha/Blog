using Blog.Application.Core.Categories.Queries;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using MediatR;

namespace Blog.Application.Core.Categories.Handlers
{
    public class CategoryGetAllQueryHandler : IRequestHandler<GetCategoriesQuery, IEnumerable<Category>>
    {
        private ICategoryRepository _categoryRepository;
        public CategoryGetAllQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<Category>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetAll();
          
        }
    }
}
