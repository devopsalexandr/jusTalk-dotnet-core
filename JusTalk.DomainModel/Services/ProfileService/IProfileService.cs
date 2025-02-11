using System.Threading.Tasks;

namespace JusTalk.DomainModel
{
    public interface IProfileService
    {
        Task<UserProfile> GetProfileAsync();

        Task UpdateProfile(ProfileData profileData);
    }
}