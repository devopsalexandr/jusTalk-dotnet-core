using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace JusTalk.DomainModel.Options
{
    public class JwtAuthOptions
    {
        public string JwtSecret { get; set; }
        
        public string JwtIssuer { get; set; }
        
        public string JwtAudience { get; set; }
        
        public int LifeTime { get; set; }
        
        public SymmetricSecurityKey SymmetricSecurityKey => 
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtSecret));
    }
}