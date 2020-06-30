using System;
using System.Collections;
using System.Collections.Generic;
using JusTalk.DAL;

namespace JusTalk.DomainModel.Managers.Common.ConversationManager
{
    public class ConversationListReadModel
    {
        public int Id { get; set; }
        
        public User FirstUser { get; set; }
        
        public User SecondUser { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
    }
}