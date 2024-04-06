using Blog.Application.Core.Comments.Commands;
using Blog.Application.Core.Comments.Response;
using Blog.Application.Core.ViewModels;
using Blog.Application.Filters;
using Blog.Application.Response;

namespace Blog.Application.Core.Services.Interfaces
{
    public interface ICommentService
    {
        Task<PagedResponse<List<CommentViewModel>>> GetCommentsByPostIdAsync(int postId, PaginationFilter filter);
        Task<CommentCommandResponse> AddAsync(CommentCreateCommand command);
        Task<CommentCommandResponse> UpdateAsync(CommentUpdateCommand command);
        Task<CommentCommandResponse> RemoveAsync(CommentRemoveCommand command);
    }
}
