using System;
using System.Collections.Generic;

namespace JusTalk.DAL
{
    public class Conversation : ITimestampable
    {
        public int Id { get; set; }

        public string FirstUserId { get; set; }
        
        public User FirstUser { get; set; }
        
        public string SecondUserId { get; set; }
        
        public User SecondUser { get; set; }

        public List<Message> Messages { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
        
        public Conversation()
        {
            Messages = new List<Message>();
        }
    }
    
}