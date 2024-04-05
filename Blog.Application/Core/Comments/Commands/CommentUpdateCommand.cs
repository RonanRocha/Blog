using Blog.Application.Core.Comments.Response;
using MediatR;
using System.Security.Claims;

namespace Blog.Application.Core.Comments.Commands
{
    public class CommentUpdateCommand : IRequest<CommentCommandResponse>
    {
        public int Id { get; set; }
        public string Message { get; set; }
        private ClaimsPrincipal ClaimsPrincipal { get; set; }
        private string UserId { get; set; }

        public CommentUpdateCommand(string message)
        {
            Message = message;
        }

        public void AddAuthenticatedUser(ClaimsPrincipal user)
        {
            ClaimsPrincipal = user;
            UserId = user.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
        }

        public void SetUser(ClaimsPrincipal user)
        {
            ClaimsPrincipal = user;
            UserId = user.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
        }

        public ClaimsPrincipal GetUser()
        {
            return ClaimsPrincipal;
        }
    }
}
