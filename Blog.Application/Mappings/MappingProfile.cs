using AutoMapper;
using Blog.Application.Account.Commands;
using Blog.Application.Core.ViewModels;
using Blog.Application.Response;
using Blog.Domain.Account.Entities;
using Blog.Domain.Core.Entities;

namespace Blog.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Post, PostViewModel>().ReverseMap();
            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<PagedResponse<List<Post>>, PagedResponse<List<PostViewModel>>>().ReverseMap();
            CreateMap<Comment, CommentViewModel>().ReverseMap();
            CreateMap<User,UserViewModel>().ReverseMap();
            CreateMap<LoginCommand,Login>().ReverseMap();   


        }
    }
}
