
using AutoMapper;
using SocialNetwork.Core.Application.Dtos.Account.Request;
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
            #region Users "Identity"
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, UserSaveViewModel>()
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            #endregion

            #region Post
            CreateMap<Post, PostSaveViewModel>()
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Comments, opt => opt.Ignore())
                .ForMember(x => x.PostId, opt => opt.Ignore());

            CreateMap<Post, PostUpdateViewModel>()
                .ForMember(x => x.Image, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.Comments, opt => opt.Ignore()); ;

            CreateMap<Post, PostViewModel>()
                .ReverseMap();
            #endregion

            #region Comment
            CreateMap<Comment, CommentSaveViewModel>()
                .ReverseMap()
                .ForMember(x => x.CommentId, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.Post, opt => opt.Ignore());

            CreateMap<Comment, CommentViewModel>()
                .ReverseMap();
            #endregion

            #region Friends
            CreateMap<Friends, FriendsSaveViewModel>()
                .ForMember(x => x.FriendUserName, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.FriendsId, opt => opt.Ignore());

            CreateMap<Friends, FriendsViewModel>()
                .ReverseMap();
;
            #endregion
        }
    }
}
