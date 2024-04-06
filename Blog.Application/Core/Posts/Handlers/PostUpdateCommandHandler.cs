using Blog.Application.Core.Posts.Commands;
using Blog.Application.Core.Posts.Response;
using Blog.Domain.Core.Entities;
using Blog.Application.Core.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Blog.Domain.Core.Uploads;

namespace Blog.Application.Core.Posts.Handlers
{
    public class PostUpdateCommandHandler : IRequestHandler<PostUpdateCommand, PostCommandResponse>
    {
        private IAuthorizationService _authorizationService;
        private readonly IPostRepository _postRepository;
        IValidator<Post> _validator;
        private IFileHandler _fileHandler;


        public PostUpdateCommandHandler(IPostRepository postRepository,
            IValidator<Post> validator,
            IAuthorizationService authorizationService,
            IFileHandler fileHandler
          )
        {
            _postRepository = postRepository;
            _validator = validator;
            _authorizationService = authorizationService;
            _fileHandler = fileHandler;
    
        }

        public async Task<PostCommandResponse> Handle(PostUpdateCommand request, CancellationToken cancellationToken)
        {
            PostCommandResponse response;

            try
            {
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

                var pathUpload = Path.Combine(_fileHandler.BasePath, "Uploads/");

                UploadResult uploadResult = await _fileHandler.UpdateImageAsync(post.Image, request.Image, Path.Combine(pathUpload, "Posts/"));

                if (!uploadResult.IsValid) return response = new PostCommandResponse
                {
                    StatusCode = 400,
                    Message = "Error on processing file",
                    IsValid = false
                };

                post.UpdatePost(
                    userId: request.GetUserId(),
                    categoryId: request.CategoryId,
                    image: uploadResult.UploadedPath,
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
            catch (Exception ex)
            {
                return new PostCommandResponse
                {
                    IsValid = false,
                    StatusCode = 500,
                    Message = ex.Message,
                };
            }
        }
    }
}
