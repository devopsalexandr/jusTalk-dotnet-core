using System.Threading.Tasks;
using JusTalk.DAL;

namespace JusTalk.DomainModel.Managers.Common
{
    public interface IUserManager
    { 
        Task AddAsync(User user);

        Task<User> FindByPhoneAsync(string phoneNumber);
    }
}