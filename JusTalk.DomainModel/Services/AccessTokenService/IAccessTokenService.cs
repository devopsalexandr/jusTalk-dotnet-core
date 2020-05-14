using System.Threading.Tasks;
using JusTalk.DAL.Entities;

namespace JusTalk.DomainModel
{
    public interface IAccessTokenService
    {
        string GenerateAccessToken(User user);
    }
}