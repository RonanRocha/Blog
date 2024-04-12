using Blog.Application.Core.Posts.Commands;
using Blog.Domain.Core.Entities;
using Blog.Application.Core.Repositories;
using MediatR;
using Blog.Domain.Core.Uploads;
using Microsoft.AspNetCore.Hosting;
using Blog.Application.Response;

namespace Blog.Application.Core.Posts.Handlers
{
    public class PostCreateCommandHandler : IRequestHandler<PostCreateCommand, ResponseBase>
    {
        private readonly IPostRepository _postRepository;
        private readonly IFileHandler _uploadFileHandler;
        private readonly IHostingEnvironment _environment;

        public PostCreateCommandHandler(
            IPostRepository postRepository,
            IFileHandler uploadFileHandler,
            IHostingEnvironment environment)
        {
     
            _uploadFileHandler = uploadFileHandler;
            _postRepository = postRepository;
            _environment = environment;
        }

        public async Task<ResponseBase> Handle(PostCreateCommand request,CancellationToken cancellationToken)
        {
            ResponseBase response;

            try
            {


                UploadResult result = await _uploadFileHandler.UploadAsync(request.Image, "Uploads/Posts");

                if (!result.IsValid) return new ResponseBase
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


                await _postRepository.Save(post);

                return new ResponseBase
                {
                    IsValid = true,
                    Message = "Post created successfuly",
                    StatusCode = 201
                };

            }
            catch (Exception ex)
            {

                return response = new ResponseBase
                {
                    Message = ex.Message,
                    StatusCode = 500,
                    IsValid = false
                };
            }

        }
    }
}
