using Microsoft.AspNetCore.Authorization;

namespace Blog.Api.Policies.Comments.Requirements
{
    public class CommentOwnerRequirement : IAuthorizationRequirement
    {
    }
}
