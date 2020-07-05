using AutoMapper;
using JusTalk.DAL;
using JusTalk.DomainModel;
using JusTalk.Web.Contracts.v1.Requests.Search;

namespace JusTalk.Web.Contracts.v1.AutoMapper
{
    public class MemberProfile : Profile
    {
        public MemberProfile()
        {
            CreateMap<User, MemberReadModel>();
            CreateMap<SearchRequest, MemberSearchFilters>();
        }
    }
}