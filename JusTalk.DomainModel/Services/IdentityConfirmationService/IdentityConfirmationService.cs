using System;
using System.Threading.Tasks;
using JusTalk.DAL;
using JusTalk.DAL.Entities;

namespace JusTalk.DomainModel.Services.IdentityConfirmationService
{
    public class IdentityConfirmationService : IIdentityConfirmationService
    {
        private readonly ApplicationContext _dbContext;
        
        protected const int MaxMinutesCodeAlive = 5;

        
        public IdentityConfirmationService(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ValidateUserForAuth(User user)
        {
            var minutesRemaining = CalculateCodeMinutesRemaining(user.UpdatedAt);

            if (minutesRemaining >= MaxMinutesCodeAlive)
                return false;

            await MakeAuthCodeEmpty(user);
            
            return true;
        }

        public async Task<string> SetRandomCodeToUser(User user)
        {
            var code = GenerateRandomCode();

            await SetAuthCodeAsync(user, code);

            return code;
        }
        
        public async Task SetAuthCodeAsync(User user, string code)
        {
            user.AuthCode = code;
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
        
        public Task MakeAuthCodeEmpty(User user)
        {
            return SetAuthCodeAsync(user, null);
        }
        
        private static string GenerateRandomCode()
        {
            return new Random().Next(100000, 999999).ToString();
        }
        
        private static int CalculateCodeMinutesRemaining(DateTime userTime)
        {
            var currentTime = DateTime.Now;

            var ct = currentTime - userTime;

            return ct.Minutes;
        }
    }
}