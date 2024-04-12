using Blog.Application.Response;
using MediatR;
using System.Security.Claims;

namespace Blog.Application.Core.Categories.Commands
{
    public class CategoryRemoveCommand : IRequest<ResponseBase>
    {
        public int Id { get; set; }
        private ClaimsPrincipal ClaimsPrincipal { get; set; }

        public CategoryRemoveCommand(int id, ClaimsPrincipal claimsPrincipal)
        {
          ClaimsPrincipal = claimsPrincipal;
          Id = id;    
        }
    }
}
