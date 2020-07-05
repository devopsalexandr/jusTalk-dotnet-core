using System.Threading.Tasks;
using JusTalk.DAL;

namespace JusTalk.DomainModel.Managers.Common.UserManger
{
    public interface IUserManager
    { 
        Task AddAsync(User user);

        Task<User> FindByPhoneAsync(string phoneNumber);

        public Task<UserReadModel> GetById(string id);
    }
}