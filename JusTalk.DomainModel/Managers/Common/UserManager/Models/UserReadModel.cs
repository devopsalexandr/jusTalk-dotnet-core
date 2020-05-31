using System;
using JusTalk.DAL;

namespace JusTalk.DomainModel.Managers.Common.Models
{
    public class UserReadModel
    {
        public string Id { get; set; }
        
        public virtual string Phone { get; set; }

        public virtual string Name { get; set; }
        
        public virtual GenderType Gender { get; set; }

        public virtual DateTime CreatedAt { get; set; }
        
        public virtual DateTime UpdatedAt { get; set; }
    }
}