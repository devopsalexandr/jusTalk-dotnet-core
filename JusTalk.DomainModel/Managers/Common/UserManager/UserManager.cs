using System;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JusTalk.DAL;
using JusTalk.DomainModel.Managers.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace JusTalk.DomainModel.Managers.Common
{
    public class UserManager : IUserManager
    {
        private readonly ApplicationContext _dbContext;

        private readonly IConfigurationProvider _mapperConfiguration;

        public UserManager(ApplicationContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapperConfiguration = mapper != null ? mapper.ConfigurationProvider : throw new ArgumentNullException(nameof(mapper));
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

        public Task<UserReadModel> GetById(string id)
        {
            return _dbContext.Users.ProjectTo<UserReadModel>(_mapperConfiguration)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}