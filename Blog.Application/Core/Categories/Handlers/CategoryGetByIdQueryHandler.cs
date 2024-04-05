using Blog.Application.Core.Categories.Queries;
using Blog.Domain.Core.Entities;
using Blog.Domain.Core.Repositories;
using MediatR;

namespace Blog.Application.Core.Categories.Handlers
{
    public class CategoryGetByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Category>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryGetByIdQueryHandler(ICategoryRepository categoryRepository)
        {
                _categoryRepository = categoryRepository;
        }
        public async Task<Category> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            return await _categoryRepository.GetById(request.Id);
        }
    }
}
