using System;

namespace JusTalk.DAL
{
    public class Message : ITimestampable
    {
        public int Id { get; set; }

        public string Text { get; set; }
        
        public User UserToId { get; set; }
        
        public User UserFromId { get; set; }

        public virtual DateTime CreatedAt { get; set; }
        
        public virtual DateTime UpdatedAt { get; set; } 
    }
}