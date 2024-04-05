using Microsoft.AspNetCore.Authorization;

namespace Blog.Api.Policies.Posts.Requirements
{
    public class PostOwnerRequirement : IAuthorizationRequirement
    {
    }
}
