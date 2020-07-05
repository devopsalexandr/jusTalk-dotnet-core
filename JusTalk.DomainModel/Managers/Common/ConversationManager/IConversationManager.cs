using System.Collections.Generic;
using System.Threading.Tasks;
using JusTalk.DAL;
using JusTalk.DomainModel.Managers.Common.ConversationManager;

namespace JusTalk.DomainModel.Managers.Common.ConversationManager
{
    public interface IConversationManager
    {
        Task<PaginationInfo<ConversationListReadModel>> GetConversationsListAsync(int currentPage = 1, int countPerPage = 10);

        Task<MessageReadModel> SendMessageAsync(SendMessageData sendMessageData);
        
        Task<PaginationInfo<MessageReadModel>> GetConversationMessagesAsync(int id, int currentPage = 1, int countPerPage = 10);
    }
}