using System.Linq;
using JusTalk.DAL;
using JusTalk.DomainModel.Managers.Common.ConversationManager;

namespace JusTalk.DomainModel.Managers.Common.Mappings
{
    public static class ConversationQueryableMapping
    {
        public static IQueryable<ConversationListReadModel> ToConversationListReadModel(this IQueryable<Conversation> source) =>
            source.Select(c => new ConversationListReadModel()
            {
                Id = c.Id,
                FirstUser = c.FirstUser,
                SecondUser = c.SecondUser,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            });
        
        public static IQueryable<MessageReadModel> ToMessageReadModel(this IQueryable<Message> source) =>
            source.Select(m => new MessageReadModel()
            {
                Id = m.Id,
                Text = m.Text,
                UserId = m.UserId,
                CreatedAt = m.CreatedAt,
                UpdatedAt = m.UpdatedAt
            });
    }
}