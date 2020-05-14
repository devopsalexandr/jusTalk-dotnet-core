using System;
using System.Threading.Tasks;

namespace JusTalk.DomainModel
{
    public class AuthService : IAuthService
    {
        private readonly IUserManager _userManager;

        public AuthService(IUserManager userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
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
            // 1. Find user by phone and code
            // 2. check code minutes


            throw new MethodAccessException();
        }
        
        private static string GenerateRandomCode()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}