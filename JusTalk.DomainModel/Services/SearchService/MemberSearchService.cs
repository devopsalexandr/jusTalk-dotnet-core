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
            var searchQuery = CreateQueryByFilters(_dbContext.Users, memberSearchFilters);

            return PaginateAsync(searchQuery, currentPage, countPerPage);
        }

        private IQueryable<User> CreateQueryByFilters(IQueryable<User> dbSet, MemberSearchFilters memberSearchFilters)
        {
            var queryBuilder = dbSet.Where(u => u.Id != _securityService.GetUserId());

            if (memberSearchFilters.Gender.HasValue) 
                queryBuilder = queryBuilder.WhereGender((GenderType) memberSearchFilters.Gender);

            return queryBuilder;
        }

        private async Task<PaginationInfo<MemberReadModel>> PaginateAsync(IQueryable<User> searchQuery, int currentPage = 1, int countPerPage = 10)
        {
            var membersCount = await searchQuery.CountAsync();
            var totalPage = (int)Math.Ceiling((float)membersCount / countPerPage);
            
            if (totalPage < 1) totalPage = 1;
            if (currentPage < 1) currentPage = 1;
            if (totalPage < currentPage) currentPage = totalPage;
            if (countPerPage < 1) countPerPage = 10;
            
            var members = await searchQuery
                .ProjectTo<MemberReadModel>(_mapperConfiguration)
                .Skip((currentPage - 1) * countPerPage)
                .Take(countPerPage)
                .ToListAsync();
            
            return new PaginationInfo<MemberReadModel>()
            {
                Entities = members,
                CurrentPage = currentPage,
                CountPerPage = countPerPage,
                TotalEntities = membersCount
            };
        }
    }
}