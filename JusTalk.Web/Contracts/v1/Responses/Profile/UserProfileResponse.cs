using System;
using JusTalk.DAL;

namespace JusTalk.Web.Contracts.v1.Responses.Profile
{
    public class UserProfileResponse
    {
        public string Id { get; set; }
        
        public string Phone { get; set; }

        public string Name { get; set; }
        
        public GenderType Gender { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
    }
}