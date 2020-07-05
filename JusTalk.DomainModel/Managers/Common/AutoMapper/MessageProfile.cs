using AutoMapper;
using JusTalk.DAL;
using JusTalk.DomainModel.Managers.Common.ConversationManager;


namespace JusTalk.DomainModel.Managers.Common.AutoMapper
{
    public class MessageProfile : Profile
    {
        public MessageProfile()
        {
            CreateMap<Message, MessageReadModel>();
            
            CreateMap<Conversation, ConversationListReadModel>();
        }
    }
}