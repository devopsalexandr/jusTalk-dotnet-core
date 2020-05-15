using AutoMapper;
using JusTalk.DAL.Entities;
using JusTalk.DomainModel.Managers.Common.Models;

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