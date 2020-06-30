using System;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using JusTalk.DAL;
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

        public Task<PaginationInfo<ConversationListReadModel>> GetConversationsAsync(int currentPage = 1, int countPerPage = 10)
        {
            var authUserId = _securityService.GetUserId();

            var conversationsQuery = _dbContext.Conversations
                .Where(c => c.FirstUser.Id == authUserId || c.SecondUser.Id == authUserId);


            return PaginateAsync(conversationsQuery, currentPage, countPerPage);
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
        
        private async Task<PaginationInfo<ConversationListReadModel>> PaginateAsync(IQueryable<Conversation> searchQuery, int currentPage = 1, int countPerPage = 10)
        {
            var entitiesCount = await searchQuery.CountAsync();
            var totalPage = (int)Math.Ceiling((float)entitiesCount / countPerPage);
            
            if (totalPage < 1) totalPage = 1;
            if (currentPage < 1) currentPage = 1;
            if (totalPage < currentPage) currentPage = totalPage;
            if (countPerPage < 1) countPerPage = 10;
            
            var entities = await searchQuery
                .ProjectTo<ConversationListReadModel>(_mapperConfiguration)
                .Skip((currentPage - 1) * countPerPage)
                .Take(countPerPage)
                .ToListAsync();
            
            return new PaginationInfo<ConversationListReadModel>()
            {
                Entities = entities,
                CurrentPage = currentPage,
                CountPerPage = countPerPage,
                TotalEntities = entitiesCount
            };
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