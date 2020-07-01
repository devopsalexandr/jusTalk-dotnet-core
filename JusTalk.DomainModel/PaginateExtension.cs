using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JusTalk.DomainModel.Managers.Common.ConversationManager;
using Microsoft.EntityFrameworkCore;

namespace JusTalk.DomainModel
{
    public static class PaginateExtension
    {
        public static async Task<PaginationInfo<T>> PaginateAsync<T>(this IQueryable<T> searchQuery, int currentPage = 1, int countPerPage = 10)
        {
            var entitiesCount = await searchQuery.CountAsync();
            var totalPage = (int)Math.Ceiling((float)entitiesCount / countPerPage);
            
            if (totalPage < 1) totalPage = 1;
            if (currentPage < 1) currentPage = 1;
            if (totalPage < currentPage) currentPage = totalPage;
            if (countPerPage < 1) countPerPage = 10;
            
            var entities = await searchQuery
                .Skip((currentPage - 1) * countPerPage)
                .Take(countPerPage)
                .ToListAsync();
            
            return new PaginationInfo<T>()
            {
                Entities = entities,
                CurrentPage = currentPage,
                CountPerPage = countPerPage,
                TotalEntities = entitiesCount
            };
        }
    }
}