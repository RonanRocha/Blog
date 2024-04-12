using Blog.Application.Response;
using MediatR;
using System.Security.Claims;

namespace Blog.Application.Core.Posts.Commands
{
    public class PostRemoveCommand : IRequest<ResponseBase>
    {
        public PostRemoveCommand(int id, ClaimsPrincipal user)
        {
            Id = id;
            User = user;    
        }

        public int Id { get; set; }

        private ClaimsPrincipal User { get; set; }

        public ClaimsPrincipal GetClaimsPrincipal()
        {
            return User;
        }
    }
}
