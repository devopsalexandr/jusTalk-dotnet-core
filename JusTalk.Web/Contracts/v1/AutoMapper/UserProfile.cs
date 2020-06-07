using AutoMapper;
using JusTalk.DAL;
using JusTalk.DomainModel;
using JusTalk.DomainModel.Managers.Common.Models;
using JusTalk.Web.Contracts.v1.Requests.Profile;
using JusTalk.Web.Contracts.v1.Responses.Profile;

namespace JusTalk.Web.Contracts.v1.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, DomainModel.UserProfile>();
            CreateMap<DomainModel.UserProfile, UserProfileResponse>();
            CreateMap<User, UserReadModel>();
            CreateMap<UserReadModel, PublicProfileResponse>();
            CreateMap<UpdateAuthUserRequest, ProfileData>();
            
            CreateMap<User, MemberReadModel>();
        }
    }
}