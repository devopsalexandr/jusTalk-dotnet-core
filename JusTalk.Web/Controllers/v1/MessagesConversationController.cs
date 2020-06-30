using System;
using System.Threading.Tasks;
using AutoMapper;
using JusTalk.DAL;
using JusTalk.DomainModel.Managers.Common.ConversationManager;
using JusTalk.Web.Contracts.v1;
using JusTalk.Web.Contracts.v1.Requests.Conversation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JusTalk.Web.Controllers.v1
{
    [Authorize]
    public class MessagesConversationController : ApiController
    {
        private readonly IMapper _mapper;
        
        private readonly IConversationManager _conversationManager;

        public MessagesConversationController(IConversationManager conversationManager, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _conversationManager = conversationManager ?? throw new ArgumentNullException(nameof(conversationManager));
        }

        [HttpPost(ApiRoutes.Conversations.Index)]
        public async Task<IActionResult> Create(SendMessageRequest request)
        {
            var messageData = _mapper.Map<SendMessageData>(request);
            var message = await _conversationManager.SendMessageAsync(messageData);

            return Ok(message);
        }
    }
}