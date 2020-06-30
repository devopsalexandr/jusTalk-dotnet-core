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
    }
}