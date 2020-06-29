using System;
using System.Threading.Tasks;
using JusTalk.DomainModel.Managers.Common.MessageManager;
using JusTalk.Web.Contracts.v1;
using Microsoft.AspNetCore.Mvc;

namespace JusTalk.Web.Controllers.v1
{
    public class ConversationsController : ApiController
    {
        private readonly IConversationManager _conversationManager;
        
        public ConversationsController(IConversationManager messageManager)
        {
            _conversationManager = messageManager ?? throw new ArgumentNullException(nameof(messageManager));
        }

        [HttpGet(ApiRoutes.Conversations.Index)]
        public async Task<IActionResult> Index()
        {
            // var conversations = await _conversationManager.GetConversations();
            return Ok();
        }
    }
}