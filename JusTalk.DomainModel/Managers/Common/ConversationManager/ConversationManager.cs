using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JusTalk.DAL;
using JusTalk.DomainModel.Managers.Common.MessageManager.Models;
using Microsoft.EntityFrameworkCore;

namespace JusTalk.DomainModel.Managers.Common.MessageManager
{
    public class ConversationManager : IConversationManager
    {
        private readonly ApplicationContext _dbContext;
        
        private readonly ISecurityService _securityService;
        
        private readonly IMapper _mapper;

        public ConversationManager(ApplicationContext dbContext, ISecurityService securityService, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            
            _securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
            
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        // public async GetConversationsAsync(int lastCount = 10)
        // {
        //     var authUserId = _securityService.GetUserId();
        //     
        //     
        // }

        public async Task<MessageReadModel> SendMessageAsync(SendMessageData sendMessageData)
        {
            var authUserId = _securityService.GetUserId();

            var receiverId = sendMessageData.ReceiverId;

            var conversation = await _dbContext.Conversations
                .Where(c => c.FirstUser.Id == authUserId && c.SecondUser.Id == receiverId || c.FirstUser.Id == receiverId && c.SecondUser.Id == authUserId)
                .FirstOrDefaultAsync();

            if (conversation == null)
            {
                conversation = new Conversation()
                {
                    FirstUserId = authUserId,
                    SecondUserId = receiverId
                };
                
                await _dbContext.Conversations.AddAsync(conversation);
                
                await _dbContext.SaveChangesAsync();
            }

            var message = new Message()
            {
                Text = sendMessageData.Text,
                ConversationId = conversation.Id,
                User = new User() { Id = authUserId }
            };

            await _dbContext.Messages.AddAsync(message);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<MessageReadModel>(message);
        }
    }
}