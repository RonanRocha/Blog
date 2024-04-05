using Blog.Application.Core.Posts.Commands;
using Blog.Application.Core.Posts.Response;
using Blog.Domain.Core.Entities;
using Blog.Application.Core.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Application.Core.Posts.Handlers
{
    public class PostUpdateCommandHandler : IRequestHandler<PostUpdateCommand, PostCommandResponse>
    {
        private IAuthorizationService _authorizationService;
        private readonly IPostRepository _postRepository;
        IValidator<Post> _validator;

        public PostUpdateCommandHandler(IPostRepository postRepository, IValidator<Post> validator, IAuthorizationService authorizationService)
        {
            _postRepository = postRepository;
            _validator = validator;
            _authorizationService = authorizationService;
        }

        public async Task<PostCommandResponse> Handle(PostUpdateCommand request, CancellationToken cancellationToken)
        {
            PostCommandResponse response;

            Post post = await _postRepository.GetById(request.Id);

            AuthorizationResult result = await _authorizationService.AuthorizeAsync(request.GetUser(), post, "EditPost");

            if (!result.Succeeded) return response = new PostCommandResponse
            {
                StatusCode = 401,
                Message = "Unathorized",
                IsValid = false
            };

            if (post == null) return new PostCommandResponse
            {
                IsValid = false,
                Message = "Post not found"
            };

            post.UpdatePost(
                userId: request.GetUserId(),
                categoryId: request.CategoryId,
                image: "",
                title: request.Title,
                content: request.Content
            );

            ValidationResult vr = _validator.Validate(post);

            if (!vr.IsValid) return response = new PostCommandResponse
            {
                IsValid = false,
                Errors = vr.ToDictionary(),
                Message = "Validation error"
            };

            await _postRepository.Update(post);

            return await Task.FromResult(new PostCommandResponse
            {
                IsValid = true,
                StatusCode = 204,
                Message = "Post updated succesfuly"
            });

        }
    }
}
