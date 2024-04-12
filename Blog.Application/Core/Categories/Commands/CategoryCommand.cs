using Blog.Application.Response;
using MediatR;
using System.Security.Claims;

namespace Blog.Application.Core.Categories.Commands
{
    public class CategoryCommand : IRequest<ResponseBase>
    {
        public string Name { get; set; }
        private ClaimsPrincipal ClaimsPrincipal { get; set; }

        public void AddAuthenticatedUser(ClaimsPrincipal claimsPrincipal)
        {
            ClaimsPrincipal = claimsPrincipal;  
        }
    }
}
