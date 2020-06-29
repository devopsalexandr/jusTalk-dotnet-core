using System.Collections.Generic;
using System.Threading.Tasks;
using JusTalk.DAL;
using JusTalk.DomainModel.Managers.Common.MessageManager.Models;

namespace JusTalk.DomainModel.Managers.Common.MessageManager
{
    public interface IConversationManager
    {
        // Task GetConversationsAsync(int lastCount = 10);

        Task<MessageReadModel> SendMessageAsync(SendMessageData sendMessageData);
    }
}