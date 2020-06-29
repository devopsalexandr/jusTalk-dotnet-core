using AutoMapper;
using JusTalk.DAL;
using JusTalk.DomainModel.Managers.Common.MessageManager.Models;
using JusTalk.DomainModel.Managers.Common.Models;

namespace JusTalk.DomainModel.Managers.Common.AutoMapper
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageReadModel>();
        }
    }
}