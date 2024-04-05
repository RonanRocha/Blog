using Blog.Application.Core.Categories.Response;
using MediatR;
using System.Security.Claims;

namespace Blog.Application.Core.Categories.Commands
{
    public class CategoryCommand : IRequest<CategoryCommandResponse>
    {
        public string Name { get; set; }
        private ClaimsPrincipal ClaimsPrincipal { get; set; }

        public void AddAuthenticatedUser(ClaimsPrincipal claimsPrincipal)
        {
            ClaimsPrincipal = claimsPrincipal;  
        }
    }
}
