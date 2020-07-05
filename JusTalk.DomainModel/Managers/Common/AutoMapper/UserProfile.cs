using AutoMapper;
using JusTalk.DAL;
using JusTalk.DomainModel.Managers.Common.UserManger;

namespace JusTalk.DomainModel.Managers.Common.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserReadModel>();
        }
    }
}