using Blog.Application.Core.Posts.Commands;
using Blog.Application.Core.Posts.Response;
using Blog.Domain.Core.Entities;
using Blog.Application.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Blog.Domain.Core.Uploads;

namespace Blog.Application.Core.Posts.Handlers
{
    public class PostRemoveCommandHandler : IRequestHandler<PostRemoveCommand, PostCommandResponse>
    {
        private readonly IPostRepository _postRepository;
        private readonly IAuthorizationService _authorizationService;
        private readonly IFileHandler _fileHandler;



        public PostRemoveCommandHandler(IPostRepository postRepository, IAuthorizationService authorizationService,  IFileHandler fileHandler)
        {
            _postRepository = postRepository;
            _authorizationService = authorizationService;
            _fileHandler = fileHandler;
        }

        public async Task<PostCommandResponse> Handle(PostRemoveCommand request, CancellationToken cancellationToken)
        {
            PostCommandResponse response;

            try
            {
                Post post = await _postRepository.GetById(request.Id);

                AuthorizationResult result = await _authorizationService.AuthorizeAsync(request.GetClaimsPrincipal(), post, "DeletePost");

                if (!result.Succeeded) return response = new PostCommandResponse
                {
                    StatusCode = 401,
                    Message = "Unathorized",
                    IsValid = false
                };

                if (post == null) return response = new PostCommandResponse
                {
                    StatusCode = 404,
                    Message = "Resource not found"
                };

                var filePath = Path.Combine(_fileHandler.BasePath, @$"Uploads/Posts/{Path.GetFileName(post.Image)}");

                
                await _postRepository.RemoveAsync(post);

                await _fileHandler.DeleteFileAsync(filePath);

                return response = new PostCommandResponse
                {
                    IsValid = true,
                    StatusCode = 200,
                    Message = "Post removed successfuly"
                };
            }
            catch (Exception ex)
            {
                return response = new PostCommandResponse
                {
                    IsValid = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }


        }
    }
}
