using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JusTalk.DAL;
using JusTalk.DomainModel.Additional.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace JusTalk.DomainModel
{
    public class MemberSearchService : IMemberSearchService
    {
        private readonly ApplicationContext _dbContext;
        
        private readonly IConfigurationProvider _mapperConfiguration;
        
        private readonly ISecurityService _securityService;
        
        public MemberSearchService(ApplicationContext dbContext, IMapper mapper, ISecurityService securityService)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapperConfiguration = mapper != null ? mapper.ConfigurationProvider : throw new ArgumentNullException(nameof(mapper));
            _securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
        }

        public Task<PaginationInfo<MemberReadModel>> Find(MemberSearchFilters memberSearchFilters, int currentPage = 1, int countPerPage = 10)
        {
            return CreateQueryByFilters(_dbContext.Users, memberSearchFilters)
                .ProjectTo<MemberReadModel>(_mapperConfiguration)
                .PaginateAsync(currentPage, countPerPage);

        }

        private IQueryable<User> CreateQueryByFilters(IQueryable<User> dbSet, MemberSearchFilters memberSearchFilters)
        {
            var queryBuilder = dbSet.Where(u => u.Id != _securityService.GetUserId());

            if (memberSearchFilters.Gender.HasValue) 
                queryBuilder = queryBuilder.WhereGender((GenderType) memberSearchFilters.Gender);

            return queryBuilder;
        }
    }
}