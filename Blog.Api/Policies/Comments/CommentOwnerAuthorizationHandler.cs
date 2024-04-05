using Blog.Api.Policies.Comments.Requirements;
using Blog.Domain.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Blog.Api.Policies.Comments
{
    public class CommentOwnerAuthorizationHandler : AuthorizationHandler<CommentOwnerRequirement, Comment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CommentOwnerRequirement requirement, Comment resource)
        {
            string? userId = context.User.Claims.FirstOrDefault(x => x.Type == "UserId")?.Value;

            var roles = context.User.Claims.Where(x => x.Type == ClaimTypes.Role);

            if (String.IsNullOrEmpty(userId)) context.Fail();

            if (resource.UserId == userId || roles.Any(x => x.Value == "Admin"))
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.CompletedTask;
        }
    }
}
