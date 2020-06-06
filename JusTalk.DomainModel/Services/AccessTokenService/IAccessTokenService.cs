using System.Threading.Tasks;
using JusTalk.DAL;

namespace JusTalk.DomainModel
{
    public interface IAccessTokenService
    {
        string GenerateAccessToken(User user);
    }
}