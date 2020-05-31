using JusTalk.DAL;

namespace JusTalk.DomainModel
{
    public class ProfileWriteModel
    {
        public string Name { get; set; }
        
        public GenderType Gender { get; set; }
    }
}