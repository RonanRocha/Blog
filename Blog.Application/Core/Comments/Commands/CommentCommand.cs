
using Blog.Application.Response;
using MediatR;
using System.Security.Claims;

namespace Blog.Application.Core.Comments.Commands
{
    public class CommentCommand : IRequest<ResponseBase>
    {
        public CommentCommand()
        {

        }
        public CommentCommand(string userId, int postId, string message)
        {
            UserId = userId;
            PostId = postId;
            Message = message;
        }

        public string UserId { get; protected set; }
        public int PostId { get; set; }
        public string Message { get; set; }
        private ClaimsPrincipal User { get; set; }

        public void AddAuthenticatedUser(ClaimsPrincipal user)
        {
            User = user;
            UserId = user.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
        }

        public void SetUser(ClaimsPrincipal user)
        {
            User = user;
            UserId = user.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
        }

        public ClaimsPrincipal GetUser()
        {
            return User;
        }

    }
}
