using Blog.Application.Response;
using MediatR;
using System.Security.Claims;

namespace Blog.Application.Core.Comments.Commands
{
    public class CommentRemoveCommand : IRequest<ResponseBase>
    {
        public CommentRemoveCommand(int id, ClaimsPrincipal claimsPrincipal)
        {
            Id = id;
            ClaimsPrincipal = claimsPrincipal;
        }
        public int Id { get; set; }
        private ClaimsPrincipal ClaimsPrincipal { get; set; }

    }
}
