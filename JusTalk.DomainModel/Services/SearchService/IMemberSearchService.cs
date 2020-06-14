using System.Threading.Tasks;

namespace JusTalk.DomainModel
{
    public interface IMemberSearchService
    {
        Task<PaginationInfo<MemberReadModel>> Find(MemberSearchFilters memberSearchData, int currentPage = 1, int countPerPage = 10);
    }
}