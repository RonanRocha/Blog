using AutoMapper;
using Blog.Application.Core.Categories.Commands;
using Blog.Application.Core.Categories.Queries;
using Blog.Application.Core.Services.Interfaces;
using Blog.Application.Core.ViewModels;
using Blog.Application.Response;
using MediatR;

namespace Blog.Application.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public CategoryService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ResponseBase> AddAsync(CategoryCreateCommand command)
        {
           return  await _mediator.Send(command);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAll()
        {
            var categories  = await _mediator.Send(new GetCategoriesQuery());
            return _mapper.Map<IEnumerable<CategoryViewModel>>(categories);
        }

        public async Task<CategoryViewModel> GetByIdAsync(int id)
        {
            var category =  await _mediator.Send(new GetCategoryByIdQuery(id));
            return _mapper.Map<CategoryViewModel>(category);    
        }

        public async Task<ResponseBase> RemoveAsync(CategoryRemoveCommand command)
        {
           return await _mediator.Send(command);
        }

        public async Task<ResponseBase> UpdateAsync(CategoryUpdateCommand command)
        {
            return await _mediator.Send(command);
        }
    }
}
