using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JusTalk.DAL;
using JusTalk.DomainModel.Managers.Common.Mappings;
using Microsoft.EntityFrameworkCore;

namespace JusTalk.DomainModel.Managers.Common.ConversationManager
{
    public class ConversationManager : IConversationManager
    {
        private readonly ApplicationContext _dbContext;
        
        private readonly ISecurityService _securityService;
        
        private readonly IMapper _mapper;
        
        private readonly IConfigurationProvider _mapperConfiguration;

        public ConversationManager(ApplicationContext dbContext, ISecurityService securityService, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            
            _securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
            
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            
            _mapperConfiguration = mapper.ConfigurationProvider ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<PaginationInfo<MessageReadModel>> GetConversationMessagesAsync(int id, int currentPage = 1, int countPerPage = 10)
        {
            var isOwner = await isConversationOwner(id);

            if (!isOwner) 
                throw new EntityAccessException("you are not owner of this conversation");
            
            var messages = await _dbContext.Conversations.Where(c => c.Id == id)
                .SelectMany(c => c.Messages)
                .OrderBy(m => m.Id)
                .ToMessageReadModel()
                .PaginateAsync(currentPage, countPerPage);

            return messages;
        }

        public async Task<bool> isConversationOwner(int id)
        {
            var authUserId = _securityService.GetUserId();

            var conversation = await _dbContext.Conversations.Where(c => c.Id == id)
                .Where(c => c.FirstUser.Id == authUserId || c.SecondUser.Id == authUserId)
                .FirstOrDefaultAsync();

            return conversation != null;
        }

        public Task<PaginationInfo<ConversationListReadModel>> GetConversationsListAsync(int currentPage = 1, int countPerPage = 10)
        {
            var authUserId = _securityService.GetUserId();

            var conversations = _dbContext.Conversations
                .ToConversationListReadModel()
                .Where(c => c.FirstUser.Id == authUserId || c.SecondUser.Id == authUserId);

            return conversations.PaginateAsync(currentPage, countPerPage);
        }

        public async Task<MessageReadModel> SendMessageAsync(SendMessageData sendMessageData)
        {
            var authUserId = _securityService.GetUserId();

            var receiverId = sendMessageData.ReceiverId;

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var conversation = await GetOrCreateConversation(authUserId, receiverId);

                var message = new Message()
                {
                    Text = sendMessageData.Text,
                    ConversationId = conversation.Id,
                    UserId = authUserId
                };

                await _dbContext.Messages.AddAsync(message);

                await _dbContext.SaveChangesAsync();
                
                transaction.Complete();

                return _mapper.Map<MessageReadModel>(message);
            }
        }

        private async Task<Conversation> GetOrCreateConversation(string firstUser, string secondUser)
        {
            var conversation = await _dbContext.Conversations
                .Where(c => c.FirstUser.Id == firstUser && c.SecondUser.Id == secondUser || c.FirstUser.Id == secondUser && c.SecondUser.Id == firstUser)
                .FirstOrDefaultAsync();

            if (conversation != null) 
                return conversation;
            
            conversation = new Conversation()
            {
                FirstUserId = firstUser,
                SecondUserId = secondUser
            };
                
            await _dbContext.Conversations.AddAsync(conversation);
                
            await _dbContext.SaveChangesAsync();

            return conversation;
        }
    }
}