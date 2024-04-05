using Blog.Api.Policies.Posts.Requirements;
using Blog.Domain.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Blog.Api.Policies.Posts
{
    public class PostOwnerAuthorizationHandler : AuthorizationHandler<PostOwnerRequirement, Post>
    {
        protected override  Task HandleRequirementAsync(AuthorizationHandlerContext context, PostOwnerRequirement requirement, Post resource)
        {
            string? userId = context.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;
            
            var roles = context.User.Claims.Where(x => x.Type == ClaimTypes.Role);

            if (String.IsNullOrEmpty(userId)) context.Fail();

            if(resource.UserId == userId || roles.Any(x => x.Value == "Admin"))
              context.Succeed(requirement);
            else 
              context.Fail();

            return Task.CompletedTask;

        }
    }
}
