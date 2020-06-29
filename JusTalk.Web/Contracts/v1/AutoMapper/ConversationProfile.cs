using AutoMapper;
using JusTalk.DAL;
using JusTalk.DomainModel.Managers.Common.MessageManager.Models;
using JusTalk.Web.Contracts.v1.Requests.Conversation;

namespace JusTalk.Web.Contracts.v1.AutoMapper
{
    public class Conversation : Profile
    {
        public Conversation()
        {
            CreateMap<SendMessageRequest, SendMessageData>();
            CreateMap<Message, MessageReadModel>();
        }
    }
}