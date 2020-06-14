using JusTalk.DAL;

namespace JusTalk.DomainModel
{
    public class MemberReadModel
    {
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public GenderType Gender { get; set; }
    }
}