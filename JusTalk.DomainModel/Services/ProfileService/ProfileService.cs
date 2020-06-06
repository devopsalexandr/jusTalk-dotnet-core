using System;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JusTalk.DAL;
using Microsoft.EntityFrameworkCore;

namespace JusTalk.DomainModel
{
    public class ProfileService : IProfileService
    {
        private readonly ISecurityService _securityService;
        
        private readonly ApplicationContext _dbContext;
        
        private readonly IConfigurationProvider _mapperConfiguration;
        
        private readonly IMapper _mapper;

        public ProfileService(ISecurityService securityService, ApplicationContext dbContext, IMapper mapper)
        {
            _securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapperConfiguration = mapper != null ? mapper.ConfigurationProvider : throw new ArgumentNullException(nameof(mapper));
            _mapper = mapper;

        }

        public async Task<UserProfile> GetProfileAsync()
        {
            var userId = _securityService.GetUserId();
            
            var profile = await _dbContext.Users
                .ProjectTo<UserProfile>(_mapperConfiguration)
                .FirstOrDefaultAsync(x => x.Id == userId);
            
            if (profile == null)
                throw new Exception("The user is not found"); // ToDo: create Exception

            return profile;
        }

        public async Task UpdateProfile(ProfileData profileData)
        {
            var userId = _securityService.GetUserId();

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            var profileDataType = profileData.GetType();
            var userType = user.GetType();

            foreach (var profileDataProperty in profileDataType.GetProperties())
            {
                var profileDataPropertyName = profileDataProperty.Name;
                var profileDataPropertyValue = profileDataProperty.GetValue(profileData);

                var userProperty = userType.GetProperty(profileDataPropertyName);
                userProperty?.SetValue(user, profileDataPropertyValue);
            }

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }
    }
}