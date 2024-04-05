using Blog.Application.Core.Categories.Commands;
using Blog.Application.Core.Services.Interfaces;
using Blog.Application.Core.ViewModels;
using Blog.Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<CategoryViewModel> categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            CategoryViewModel categoryViewModel = await _categoryService.GetByIdAsync(id);

            if(categoryViewModel == null) 
                return NotFound("Resource not found");

            return Ok(categoryViewModel);
        }

        [HttpPost]
        [Authorize(Roles="Publisher")]
        public async Task<IActionResult> Create(CategoryCreateCommand command)
        {
            command.AddAuthenticatedUser(User);

            IResponseBase response = await _categoryService.AddAsync(command);

            return GetActionResult(response);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles="Publisher")]
        public async Task<IActionResult> Update([FromBody] CategoryUpdateCommand command, int id)
        {
            if(id <= 0 || id != command.Id) 
                return BadRequest("Validation error");

            command.AddAuthenticatedUser(User);

            IResponseBase response = await _categoryService.UpdateAsync(command);

            return GetActionResult(response);
    
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles="Publisher")]
        public async Task<IActionResult> Remove([FromBody] CategoryRemoveCommand command, int id)
        {
            if (id <= 0 || id != command.Id) 
                return BadRequest("Validation error");

            IResponseBase response = await _categoryService.RemoveAsync(command);

            return GetActionResult(response);
        }


        private IActionResult GetActionResult(IResponseBase response)
        {
            switch (response.StatusCode)
            {
                case StatusCodes.Status200OK:
                    return Ok(response);
                case StatusCodes.Status201Created:
                    return Created(response.Message, response);
                case StatusCodes.Status204NoContent:
                    return StatusCode(StatusCodes.Status204NoContent);
                case StatusCodes.Status400BadRequest:
                    return BadRequest(response);
                case StatusCodes.Status401Unauthorized:
                    return Unauthorized(response);
                case StatusCodes.Status500InternalServerError:
                    return StatusCode(StatusCodes.Status500InternalServerError);
                default:
                    return UnprocessableEntity("Unprocessable entity");
            }

        }



    }
}
