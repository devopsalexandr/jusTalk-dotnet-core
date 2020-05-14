using System.Threading.Tasks;

namespace JusTalk.DomainModel
{
    public interface IAuthService
    {
        public Task<AuthResult> GetVerificationCodeAsync(string phoneNumber);

        public Task<ConfirmAuthResult> ConfirmAuthAsync(string phoneNumber, string code);
    }
}