namespace JusTalk.Web.Contracts.v1.Requests.Identity
{
    public class ConfirmPhoneRequest
    {
        public string PhoneNumber { get; set; }
        
        public string CodeVerification { get; set; }
    }
}