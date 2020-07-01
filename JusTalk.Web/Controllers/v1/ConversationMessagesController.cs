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
    public class ConversationMessagesController : ApiController
    {
        private readonly IMapper _mapper;
        
        private readonly IConversationManager _conversationManager;

        public ConversationMessagesController(IConversationManager conversationManager, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _conversationManager = conversationManager ?? throw new ArgumentNullException(nameof(conversationManager));
        }

        [HttpGet(ApiRoutes.Conversations.Messages.Index)]
        public async Task<IActionResult> Index(int id)
        {
            var messages = await _conversationManager.GetConversationMessagesAsync(id);
            
            return OkWithResult(messages);
        }

        [HttpPost(ApiRoutes.Conversations.Index)]
        public async Task<IActionResult> Create(SendMessageRequest request)
        {
            var messageData = _mapper.Map<SendMessageData>(request);
            var message = await _conversationManager.SendMessageAsync(messageData);

            return OkWithResult(message);
        }
        
    }
}