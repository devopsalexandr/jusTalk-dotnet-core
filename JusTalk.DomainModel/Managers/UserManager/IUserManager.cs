using System.Threading.Tasks;
using JusTalk.DAL.Entities;

namespace JusTalk.DomainModel
{
    public interface IUserManager
    { 
        Task AddAsync(User user);

        Task<User> FindByPhoneAsync(string phoneNumber);

        Task<User> GetOrCreateUserByPhoneAsync(string phoneNumber);

        Task SetAuthCodeAsync(User user, string code);

        Task<User> FindByPhoneAndCodeAsync(string phoneNumber, string code);

        Task MakeAuthCodeEmpty(User user);
    }
}