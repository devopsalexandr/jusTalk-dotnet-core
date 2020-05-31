using System.Threading.Tasks;
using JusTalk.DAL;
using JusTalk.DomainModel.Managers.Common.Models;

namespace JusTalk.DomainModel.Managers.Common
{
    public interface IUserManager
    { 
        Task AddAsync(User user);

        Task<User> FindByPhoneAsync(string phoneNumber);

        public Task<UserReadModel> GetById(string id);
    }
}