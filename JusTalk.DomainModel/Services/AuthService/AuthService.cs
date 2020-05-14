using System;
using System.Threading.Tasks;

namespace JusTalk.DomainModel
{
    public class AuthService : IAuthService
    {
        private readonly IUserManager _userManager;
        
        private readonly IAccessTokenService _accessTokenService;
        
        protected const int MaxMinutesCodeAlive = 5;

        public AuthService(IUserManager userManager, IAccessTokenService accessTokenService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _accessTokenService = accessTokenService ?? throw new ArgumentNullException(nameof(accessTokenService));
        }

        public async Task<AuthResult> GetVerificationCodeAsync(string phoneNumber)
        {
            var user = await _userManager.GetOrCreateUserByPhoneAsync(phoneNumber);
            
            // Todo: check isBanned? AuthResult.Failed("your account was banned")

            var code = GenerateRandomCode();
            
            await _userManager.SetAuthCodeAsync(user, code);

            return AuthResult.Success(code);
        }

        public async Task<ConfirmAuthResult> ConfirmAuthAsync(string phoneNumber, string code)
        {
            var user = await _userManager.FindByPhoneAndCodeAsync(phoneNumber, code);

            if (user == null)
                return ConfirmAuthResult.Failed("wrong phone number or code verification");
            
            var minutesRemaining = CalculateCodeMinutesRemaining(user.UpdatedAt);
            
            if (minutesRemaining >= MaxMinutesCodeAlive) 
                return ConfirmAuthResult.Failed("expired code verification");;
            
            await _userManager.MakeAuthCodeEmpty(user);

            var accessToken = _accessTokenService.GenerateAccessToken(user);

            return ConfirmAuthResult.Success(new IdentityInfo()
            {
                Id = user.Id,
                AccessToken = accessToken,
                RefreshToken = null
            });
        }
        
        private static int CalculateCodeMinutesRemaining(DateTime userTime)
        {
            var currentTime = DateTime.Now;

            var ct = currentTime - userTime;

            return ct.Minutes;
        }
        
        private static string GenerateRandomCode()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}