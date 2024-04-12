using Blog.Application.Core.Posts.Commands;
using Blog.Domain.Core.Entities;
using Blog.Application.Core.Repositories;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Blog.Domain.Core.Uploads;
using Blog.Application.Response;

namespace Blog.Application.Core.Posts.Handlers
{
    public class PostUpdateCommandHandler : IRequestHandler<PostUpdateCommand, ResponseBase>
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IPostRepository _postRepository;
        private readonly IFileHandler _fileHandler;


        public PostUpdateCommandHandler
        (
            IPostRepository postRepository,
            IAuthorizationService authorizationService,
            IFileHandler fileHandler
        )
        {
            _postRepository = postRepository;
            _authorizationService = authorizationService;
            _fileHandler = fileHandler;
        }

        public async Task<ResponseBase> Handle(PostUpdateCommand request, CancellationToken cancellationToken)
        {
            ResponseBase response;

            try
            {
                Post post = await _postRepository.GetById(request.Id);

                AuthorizationResult result = await _authorizationService.AuthorizeAsync(request.GetUser(), post, "EditPost");

                if (!result.Succeeded) return response = new ResponseBase
                {
                    StatusCode = 401,
                    Message = "Unathorized",
                    IsValid = false
                };

                if (post == null) return new ResponseBase
                {
                    IsValid = false,
                    Message = "Post not found"
                };

              

                UploadResult uploadResult = await _fileHandler.UpdateImageAsync(post.Image, request.Image);

                if (!uploadResult.IsValid) return response = new ResponseBase
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


                await _postRepository.Update(post);

                return await Task.FromResult(new ResponseBase
                {
                    IsValid = true,
                    StatusCode = 204,
                    Message = "Post updated succesfuly"
                });
            }
            catch (Exception ex)
            {
                return new ResponseBase
                {
                    IsValid = false,
                    StatusCode = 500,
                    Message = ex.Message,
                };
            }
        }
    }
}
