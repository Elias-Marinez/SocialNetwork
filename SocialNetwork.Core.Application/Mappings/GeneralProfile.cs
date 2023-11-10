
using AutoMapper;
using SocialNetwork.Core.Application.ViewModels.Comment;
using SocialNetwork.Core.Application.ViewModels.Friends;
using SocialNetwork.Core.Application.ViewModels.Post;
using SocialNetwork.Core.Application.ViewModels.User;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region Users
            CreateMap<User, UserSaveViewModel>()
                .ForMember(x => x.ConfirmPassword, opt => opt.Ignore())
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.UserId, opt => opt.Ignore())
                .ForMember(x => x.Posts, opt => opt.Ignore())
                .ForMember(x => x.Friends, opt => opt.Ignore())
                .ForMember(x => x.Comments, opt => opt.Ignore());

            CreateMap<User, UserUpdateViewModel>()
                .ForMember(dest => dest.ConfirmPassword, opt => opt.MapFrom(src => src.Password))
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Posts, opt => opt.Ignore())
                .ForMember(x => x.Friends, opt => opt.Ignore())
                .ForMember(x => x.Comments, opt => opt.Ignore());

            CreateMap<User, UserViewModel>()
                .ReverseMap();

            CreateMap<User, FriendViewModel>()
                .ReverseMap()
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ForMember(x => x.Status, opt => opt.Ignore())
                .ForMember(x => x.Comments, opt => opt.Ignore())
                .ForMember(x => x.Friends, opt => opt.Ignore());
            #endregion

            #region Post
            CreateMap<Post, PostSaveViewModel>()
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Comments, opt => opt.Ignore())
                .ForMember(x => x.UserId, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore());

            CreateMap<Post, PostUpdateViewModel>()
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Comments, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore());

            CreateMap<Post, PostViewModel>()
                .ReverseMap();
            #endregion

            #region Comment
            CreateMap<Comment, CommentSaveViewModel>()
                .ReverseMap()
                .ForMember(x => x.CommentId, opt => opt.Ignore())
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.Post, opt => opt.Ignore());

            CreateMap<Comment, CommentViewModel>()
                .ReverseMap();
            #endregion
        }
    }
}
