using System.Collections.Generic;

namespace JusTalk.DomainModel
{
    public class ConfirmAuthResult
    {
        public IdentityInfo IdentityInfo { get; }

        public bool Succeeded { get; }

        public IEnumerable<string> Errors { get; }
    }
}