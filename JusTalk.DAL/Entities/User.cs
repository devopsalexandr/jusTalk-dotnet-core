using System;

namespace JusTalk.DAL.Entities
{
    public class User : ITimestampable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}