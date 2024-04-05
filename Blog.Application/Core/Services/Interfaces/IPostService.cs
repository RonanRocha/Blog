using Blog.Application.Core.Posts.Commands;
using Blog.Application.Core.Posts.Response;
using Blog.Application.Core.ViewModels;
using Blog.Application.Filters;
using Blog.Application.Response;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Blog.Application.Core.Services.Interfaces
{
    public interface IPostService
    {
        Task<PagedResponse<List<PostViewModel>>> GetPostsAsync(PaginationFilter filter);
        Task<PostViewModel> GetByIdAsync(int id);
        Task<PostCommandResponse> AddAsync(PostCreateCommand command);
        Task<PostCommandResponse> UpdateAsync(PostUpdateCommand command);
        Task<PostCommandResponse> RemoveAsync(PostRemoveCommand command);
    }
}
