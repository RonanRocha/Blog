using Blog.Application.Core.Comments.Commands;
using Blog.Application.Core.Comments.Response;
using Blog.Application.Core.ViewModels;

namespace Blog.Application.Core.Services.Interfaces
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentViewModel>> GetCommentsByPostIdAsync(int postId);
        Task<CommentCommandResponse> AddAsync(CommentCreateCommand command);
        Task<CommentCommandResponse> UpdateAsync(CommentUpdateCommand command);
        Task<CommentCommandResponse> RemoveAsync(CommentRemoveCommand command);
    }
}
