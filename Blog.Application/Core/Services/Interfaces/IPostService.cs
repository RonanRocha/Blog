using Blog.Application.Core.Posts.Commands;
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
        Task<ResponseBase> AddAsync(PostCreateCommand command);
        Task<ResponseBase> UpdateAsync(PostUpdateCommand command);
        Task<ResponseBase> RemoveAsync(PostRemoveCommand command);
    }
}
