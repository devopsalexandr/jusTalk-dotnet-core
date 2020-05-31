using System;
using JusTalk.DAL;

namespace JusTalk.Web.Contracts.v1.Responses.Profile
{
    public class PublicProfileResponse
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public GenderType Gender { get; set; }
    }
}