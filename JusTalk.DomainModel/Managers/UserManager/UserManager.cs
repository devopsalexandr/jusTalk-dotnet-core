using System;
using System.Threading.Tasks;
using JusTalk.DAL;
using JusTalk.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace JusTalk.DomainModel
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
        
        public async Task<User> GetOrCreateUserByPhoneAsync(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException(nameof(phoneNumber));

            var user = await FindByPhoneAsync(phoneNumber);

            if (user != null) 
                return user;
            
            user = new User()
            {
                Phone = phoneNumber
            };

            await AddAsync(user);

            return user;
        }
        
        public async Task SetAuthCodeAsync(User user, string code)
        {
            user.AuthCode = code;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public Task<User> FindByPhoneAndCodeAsync(string phoneNumber, string code)
        {
            return _dbContext.Users.FirstOrDefaultAsync(u => u.Phone == phoneNumber && u.AuthCode == code);
        }
        
        public Task MakeAuthCodeEmpty(User user)
        {
            return SetAuthCodeAsync(user, null);
        }

    }
}