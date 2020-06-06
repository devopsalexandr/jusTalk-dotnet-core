using AutoMapper;
using JusTalk.DAL;

namespace JusTalk.DomainModel.Services.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, DomainModel.UserProfile>();
        }
    }
}