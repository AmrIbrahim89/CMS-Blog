using Entities;
using Microsoft.Extensions.Logging;
using RepositoryContracts;

namespace Repositories
{
    public class SubscriberRepository : ISubscribeRepository
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly ILogger<SubscriberRepository> _logger;

        public SubscriberRepository(ApplicationDBContext dBContext, ILogger<SubscriberRepository> logger)
        {
            _dbContext = dBContext;
            _logger = logger;
        }
        public async Task<Subscriber> AddSubscriber(Subscriber subscriberDetails)
        {
            _dbContext.Add(subscriberDetails);
            await _dbContext.SaveChangesAsync();

            return subscriberDetails;
        }
    }
}
