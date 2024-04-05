using Microsoft.AspNetCore.Identity;

namespace Blog.Domain.Core.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
