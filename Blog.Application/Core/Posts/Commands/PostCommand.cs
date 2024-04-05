using MediatR;
using Blog.Application.Core.Posts.Response;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Blog.Application.Core.Posts.Commands
{
    public class PostCommand : IRequest<PostCommandResponse>
    {

        public PostCommand()
        {

        }
        public PostCommand(int categoryId, string title, string content, string image)
        {
            CategoryId = categoryId;
            Title = title;
            Content = content;
            Image = image;
            UserId = User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
        }

        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        private string UserId { get;  set; }
        private ClaimsPrincipal User {  get;  set; }


        public void AddAuthenticatedUser(ClaimsPrincipal user)
        {
            SetUser(user);
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

        public string GetUserId()
        {
            return UserId;
        }

    }
}
