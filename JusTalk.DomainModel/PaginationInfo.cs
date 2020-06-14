using System.Collections.Generic;

namespace JusTalk.DomainModel
{
    public class PaginationInfo<T>
    {
        public IEnumerable<T> Entities { get; set; }

        public int CurrentPage { get; set; }

        public int CountPerPage { get; set; }

        public int TotalEntities { get; set; }
    }
}