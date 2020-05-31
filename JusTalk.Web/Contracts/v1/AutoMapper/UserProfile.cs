using AutoMapper;
using JusTalk.DAL;
using JusTalk.Web.Contracts.v1.Responses.Profile;

namespace JusTalk.Web.Contracts.v1.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, DomainModel.UserProfile>();
            CreateMap<DomainModel.UserProfile, UserProfileResponse>();
        }
    }
}