using System;
using System.Linq;
using JusTalk.DAL;

namespace JusTalk.DomainModel.Additional.QueryableExtensions
{
    public static class MemberExtension
    {
        public static IQueryable<User> WithStatus(this IQueryable<User> source)
        {
            throw new Exception();
        }
        
        public static IQueryable<User> WhereGender(this IQueryable<User> source, GenderType genderType) 
            => source.Where(x => x.Gender == genderType);
    }
}