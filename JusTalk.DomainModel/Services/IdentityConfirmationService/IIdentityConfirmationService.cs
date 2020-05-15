using System.Threading.Tasks;
using JusTalk.DAL.Entities;

namespace JusTalk.DomainModel.Services.IdentityConfirmationService
{
    public interface IIdentityConfirmationService
    {
        Task<string> SetRandomCodeToUser(User user);

        Task<bool> ValidateUserForAuth(User user);
    }
}