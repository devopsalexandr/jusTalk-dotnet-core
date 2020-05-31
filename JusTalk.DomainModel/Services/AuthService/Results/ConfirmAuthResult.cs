using System.Collections.Generic;

namespace JusTalk.DomainModel
{
    public class ConfirmAuthResult
    {
        public IdentityInfo IdentityInfo { get; }

        public bool Succeeded { get; }

        public IEnumerable<string> Errors { get; }
        
        private ConfirmAuthResult(IdentityInfo identityInfo)
        {
            IdentityInfo = identityInfo;
            Errors = null;
            Succeeded = true;
        }

        private ConfirmAuthResult(string[] errors)
        {
            Errors = errors;
            Succeeded = false;
            IdentityInfo = null;
        }
        
        public static ConfirmAuthResult Failed(params string[] errors) =>
            new ConfirmAuthResult(errors);

        public static ConfirmAuthResult Success(IdentityInfo identityInfo) =>
            new ConfirmAuthResult(identityInfo);
    }
}