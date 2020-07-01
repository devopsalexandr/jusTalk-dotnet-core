using System;
using System.Threading.Tasks;
using JusTalk.DomainModel.Managers.Common.ConversationManager;
using JusTalk.Web.Contracts.v1;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JusTalk.Web.Controllers.v1
{
    [Authorize]
    public class ConversationsController : ApiController
    {
        private readonly IConversationManager _conversationManager;
        
        public ConversationsController(IConversationManager messageManager)
        {
            _conversationManager = messageManager ?? throw new ArgumentNullException(nameof(messageManager));
        }

        [HttpGet(ApiRoutes.Conversations.Index)]
        public async Task<IActionResult> Index([FromQuery] int page = 1, int count = 10)
        {
            var conversations = await _conversationManager.GetConversationsListAsync(page, count);

            return OkWithResult(conversations);
        }
    }
}