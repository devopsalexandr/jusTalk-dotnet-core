using System;

namespace JusTalk.DAL.Entities
{
    public class User : ITimestampable
    {
        public string Id { get; set; }
        
        public virtual string Phone { get; set; }

        public virtual string Name { get; set; }
        
        public virtual string AuthCode { get; set; }

        public virtual GenderType Gender { get; set; }

        public virtual DateTime CreatedAt { get; set; }
        
        public virtual DateTime UpdatedAt { get; set; }

        public User()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}