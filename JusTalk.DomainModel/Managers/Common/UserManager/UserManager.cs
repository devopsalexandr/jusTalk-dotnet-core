using System;
using System.Threading.Tasks;
using JusTalk.DAL;
using Microsoft.EntityFrameworkCore;

namespace JusTalk.DomainModel.Managers.Common
{
    public class UserManager : IUserManager
    {
        private readonly ApplicationContext _dbContext;

        public UserManager(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public Task<User> FindByPhoneAsync(string phoneNumber)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Phone == phoneNumber);
        }
    }
}