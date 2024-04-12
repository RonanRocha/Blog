using Blog.Application.Core.Posts.Commands;
using Blog.Domain.Core.Entities;
using Blog.Application.Core.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Blog.Domain.Core.Uploads;
using Blog.Application.Response;

namespace Blog.Application.Core.Posts.Handlers
{
    public class PostRemoveCommandHandler : IRequestHandler<PostRemoveCommand, ResponseBase>
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

        public async Task<ResponseBase> Handle(PostRemoveCommand request, CancellationToken cancellationToken)
        {
            ResponseBase response;

            try
            {
                Post post = await _postRepository.GetById(request.Id);

                AuthorizationResult result = await _authorizationService.AuthorizeAsync(request.GetClaimsPrincipal(), post, "DeletePost");

                if (!result.Succeeded) return response = new ResponseBase
                {
                    StatusCode = 401,
                    Message = "Unathorized",
                    IsValid = false
                };

                if (post == null) return response = new ResponseBase
                {
                    StatusCode = 404,
                    Message = "Resource not found"
                };

                var filePath = Path.Combine(_fileHandler.BasePath, @$"Uploads/Posts/{Path.GetFileName(post.Image)}");

                
                await _postRepository.RemoveAsync(post);

                await _fileHandler.DeleteFileAsync("Uploads/Posts/",filePath);

                return response = new ResponseBase
                {
                    IsValid = true,
                    StatusCode = 200,
                    Message = "Post removed successfuly"
                };
            }
            catch (Exception ex)
            {
                return response = new ResponseBase
                {
                    IsValid = false,
                    StatusCode = 500,
                    Message = ex.Message
                };
            }


        }
    }
}
