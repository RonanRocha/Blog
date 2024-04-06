﻿using Blog.Application.Core.Comments.Commands;
using Blog.Application.Core.Comments.Response;
using Blog.Application.Core.Services.Interfaces;
using Blog.Application.Core.ViewModels;
using Blog.Application.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        private readonly ICommentService _commentService;
        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;   
        }

        [HttpGet("Post/{postId:int}")]
        [Authorize]
        public async Task<IActionResult> GetCommentsByPostId(int postId)
        {
            IEnumerable<CommentViewModel> comments = await _commentService.GetCommentsByPostIdAsync(postId);

            return Ok(comments);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CommentCreateCommand command)
        {
            command.AddAuthenticatedUser(User);

            CommentCommandResponse response =  await _commentService.AddAsync(command);

            return GetActionResult(response);
          
        }

        [HttpPut("{id:int}")]
        [Authorize(Policy="EditComment")]
        public async Task<IActionResult> Update([FromBody] CommentUpdateCommand command , int id)
        {
            if (id != command.Id  && id <= 0) 
                return BadRequest("Validation Error");

            command.AddAuthenticatedUser(User);

            IResponseBase response = await _commentService.UpdateAsync(command);

            return GetActionResult(response);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Policy="RemoveComment")]
        public async Task<IActionResult> Remove(int id)
        {
            if (id <= 0 ) 
                return BadRequest("Validation Error");

            IResponseBase response = await _commentService.RemoveAsync(new CommentRemoveCommand(id, User));

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
                    return StatusCode(StatusCodes.Status204NoContent, response);
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