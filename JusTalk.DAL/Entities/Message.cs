using System;

namespace JusTalk.DAL
{
    public class Message : ITimestampable
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public string UserId { get; set; }
        
        public User User { get; set; }

        public int ConversationId { get; set; }
        
        public Conversation Conversation { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; } 
    }
}