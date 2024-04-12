using Blog.Application.Core.Comments.Commands;
using Blog.Application.Core.ViewModels;
using Blog.Application.Filters;
using Blog.Application.Response;

namespace Blog.Application.Core.Services.Interfaces
{
    public interface ICommentService
    {
        Task<PagedResponse<List<CommentViewModel>>> GetCommentsByPostIdAsync(int postId, PaginationFilter filter);
        Task<ResponseBase> AddAsync(CommentCreateCommand command);
        Task<ResponseBase> UpdateAsync(CommentUpdateCommand command);
        Task<ResponseBase> RemoveAsync(CommentRemoveCommand command);
    }
}
