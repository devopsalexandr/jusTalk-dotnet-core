using AutoMapper;
using JusTalk.DAL;
using JusTalk.DomainModel;
using JusTalk.DomainModel.Managers.Common.Models;
using JusTalk.Web.Contracts.v1.Requests.Profile;
using JusTalk.Web.Contracts.v1.Requests.Search;
using JusTalk.Web.Contracts.v1.Responses.Profile;

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