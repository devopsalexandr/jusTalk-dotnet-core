using System;
using System.Threading.Tasks;
using JusTalk.DAL;
using JusTalk.DomainModel.Managers.Common.UserManger;
using JusTalk.DomainModel.Services.IdentityConfirmationService;
using Microsoft.EntityFrameworkCore;

namespace JusTalk.DomainModel
{
    public class AuthService : IAuthService
    {
        private readonly IUserManager _userManager;
        
        private readonly IAccessTokenService _accessTokenService;
        
        private readonly ApplicationContext _dbContext;
        
        private readonly IIdentityConfirmationService _identityConfirmationService;

        public AuthService(
            IUserManager userManager, 
            IAccessTokenService accessTokenService, 
            ApplicationContext dbContext,
            IIdentityConfirmationService identityConfirmationService
        )
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _accessTokenService = accessTokenService ?? throw new ArgumentNullException(nameof(accessTokenService));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _identityConfirmationService = identityConfirmationService ?? throw new ArgumentNullException(nameof(identityConfirmationService));
        }

        public async Task<AuthResult> GetVerificationCodeAsync(string phoneNumber)
        {
            var user = await GetOrCreateUserByPhoneAsync(phoneNumber);
            
            // Todo: check isBanned? AuthResult.Failed("your account was banned")

            var code = await _identityConfirmationService.SetRandomCodeToUser(user);
            
            return AuthResult.Success(code);
        }

        public async Task<ConfirmAuthResult> ConfirmAuthAsync(string phoneNumber, string code)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Phone == phoneNumber && u.AuthCode == code);

            if (user == null)
                return ConfirmAuthResult.Failed("wrong phone number or code verification");

            var valid = await _identityConfirmationService.ValidateUserForAuth(user);
            
            if(!valid)
                return ConfirmAuthResult.Failed("expired code verification");

            var accessToken = _accessTokenService.GenerateAccessToken(user);

            return ConfirmAuthResult.Success(new IdentityInfo()
            {
                Id = user.Id,
                AccessToken = accessToken,
                RefreshToken = null
            });
        }
        
        private async Task<User> GetOrCreateUserByPhoneAsync(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber)) throw new ArgumentNullException(nameof(phoneNumber));

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Phone == phoneNumber);

            if (user != null) 
                return user;
            
            user = new User()
            {
                Phone = phoneNumber
            };

            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }
    }
}