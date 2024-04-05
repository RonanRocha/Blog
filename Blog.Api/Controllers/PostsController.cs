using Blog.Application.Core.Posts.Commands;
using Blog.Application.Core.Posts.Response;
using Blog.Application.Core.Services.Interfaces;
using Blog.Application.Core.ViewModels;
using Blog.Application.Filters;
using Blog.Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private IPostService _postService;
     
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [Authorize(Roles = "Publisher")]
        public async Task<IActionResult> Create([FromBody] PostCreateCommand command)
        {
             command.AddAuthenticatedUser(User);
             PostCommandResponse response = await  _postService.AddAsync(command);
             return GetActionResult(response);         
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            PostViewModel viewModel = await _postService.GetByIdAsync(id);
            if(viewModel == null) return NotFound("Resource not found");   
            return Ok(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {

            if (filter.PageNumber <= 0 || filter.PageSize <= 0) 
                return BadRequest("Validation error");

            PagedResponse<List<PostViewModel>> posts = await _postService.GetPostsAsync(filter);

            return GetActionResult(posts);
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Publisher")]
        public async Task<IActionResult> Update([FromBody] PostUpdateCommand command, int id)
        {
            if (command.Id != id && id <= 0) 
                return BadRequest("Validation error");

            command.AddAuthenticatedUser(User);

            PostCommandResponse response = await _postService.UpdateAsync(command);

            return GetActionResult(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Publisher")]
        public async Task<IActionResult> Remove(int id)
        {
            if (id <= 0) 
                return BadRequest("Validation Error");
            
            PostCommandResponse response =  await _postService.RemoveAsync(new PostRemoveCommand(id, User));

            return GetActionResult(response);
        }

        private IActionResult GetActionResult(IResponseBase response)
        {
            switch (response.StatusCode)
            {
                case StatusCodes.Status200OK: 
                    return Ok(response);
                case StatusCodes.Status201Created:
                    return Created(String.Empty, response);
                case StatusCodes.Status204NoContent:
                    return Created(String.Empty, response);
                case StatusCodes.Status400BadRequest:
                    return BadRequest(response);
                case StatusCodes.Status401Unauthorized:
                    return Unauthorized(response);
                case StatusCodes.Status500InternalServerError:
                    return StatusCode(StatusCodes.Status500InternalServerError);
                case StatusCodes.Status404NotFound:
                    return NotFound(response);
                default:
                    return UnprocessableEntity("Unprocessable entity");
            }

        }

    }
}
