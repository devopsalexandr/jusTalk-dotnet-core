using JusTalk.DAL;

namespace JusTalk.Web.Contracts.v1.Requests.Profile
{
    public class UpdateAuthUserRequest
    {
        public string Name { get; set; }
        
        public GenderType Gender { get; set; }
    }
}