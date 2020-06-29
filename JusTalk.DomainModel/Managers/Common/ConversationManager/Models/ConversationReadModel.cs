using System.Collections;
using System.Collections.Generic;
using JusTalk.DAL;

namespace JusTalk.DomainModel.Managers.Common.MessageManager.Models
{
    public class ConversationReadModelReadModel
    {
        public string Key { get; set; }
        
        public IList<Message> Messages { get; set; }
    }
}