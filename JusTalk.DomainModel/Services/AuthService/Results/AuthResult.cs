using System.Collections.Generic;

namespace JusTalk.DomainModel
{
    public struct AuthResult
    {
        public string ConfirmationCode { get; }

        public bool Succeeded { get; }

        public IEnumerable<string> Errors { get; }
        
        private AuthResult(string confirmationCode)
        {
            Errors = null;
            Succeeded = true;
            ConfirmationCode = confirmationCode;
        }

        private AuthResult(string[] errors)
        {
            Errors = errors;
            Succeeded = false;
            ConfirmationCode = null;
        }

        public static AuthResult Failed(params string[] errors) =>
            new AuthResult(errors);

        public static AuthResult Success(string confirmationCode) =>
            new AuthResult(confirmationCode);
    }
}