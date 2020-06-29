using System;
using JusTalk.DAL;

namespace JusTalk.DomainModel.Managers.Common.MessageManager.Models
{
    public class MessageReadModel
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }

        // public Conversation Conversation { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; } 
    }
}