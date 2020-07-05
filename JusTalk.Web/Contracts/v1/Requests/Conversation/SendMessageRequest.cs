namespace JusTalk.Web.Contracts.v1.Requests.Conversation
{
    public class SendMessageRequest
    {
        public string ReceiverId { get; set; }
        
        public string Text { get; set; }
    }
}