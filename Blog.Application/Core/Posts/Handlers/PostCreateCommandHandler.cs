using Blog.Application.Core.Posts.Commands;
using Blog.Application.Core.Posts.Response;
using Blog.Domain.Core.Entities;
using Blog.Application.Core.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Blog.Domain.Core.Uploads;
using Microsoft.AspNetCore.Hosting;

namespace Blog.Application.Core.Posts.Handlers
{
    public class PostCreateCommandHandler : IRequestHandler<PostCreateCommand, PostCommandResponse>
    {
        private readonly IValidator<Post> _validator;
        private readonly IPostRepository _postRepository;
        private readonly IFileHandler _uploadFileHandler;
        private readonly IHostingEnvironment _environment;

        public PostCreateCommandHandler(IValidator<Post> validator,
            IPostRepository postRepository,
            IFileHandler uploadFileHandler,
            IHostingEnvironment environment)
        {
            _validator = validator;
            _uploadFileHandler = uploadFileHandler;
            _postRepository = postRepository;
            _environment = environment;
        }

        public async Task<PostCommandResponse> Handle(PostCreateCommand request,CancellationToken cancellationToken)
        {
            PostCommandResponse response;

            try
            {

                var pathUpload = Path.Combine(_environment.WebRootPath, "Uploads");
               

                UploadResult result = await _uploadFileHandler.UploadAsync(request.Image, Path.Combine(pathUpload, "Posts"));

                if (!result.IsValid) return new PostCommandResponse
                {
                    IsValid = false,
                    StatusCode = 400,
                    Message = "Failed to upload file"
                };


                Post post = new Post(userId: request.GetUserId(),
                                     categoryId: request.CategoryId,
                                     image: result.UploadedPath,
                                     title: request.Title,
                                     content: request.Content
                                    );

                ValidationResult vr = _validator.Validate(post);

                if (!vr.IsValid) return new PostCommandResponse
                {
                    Message = "Validation Error",
                    Errors = vr.ToDictionary(),
                    StatusCode = 400
                };

                await _postRepository.Save(post);

                return new PostCommandResponse
                {
                    IsValid = true,
                    Message = "Post created successfuly",
                    StatusCode = 201
                };

            }
            catch (Exception ex)
            {

                return response = new PostCommandResponse
                {
                    Message = ex.Message,
                    StatusCode = 500,
                    IsValid = false
                };
            }

        }
    }
}
