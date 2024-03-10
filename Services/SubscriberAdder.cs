using Entities;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;
using Services.Helpers;

namespace Services
{
    public class SubscriberAdder : ISubscriberAdderService
    {
        private readonly ISubscribeRepository _subscribeRepository;
        private ILogger<SubscriberAdder> _logger;

        public SubscriberAdder(ISubscribeRepository subscribeRepository, ILogger<SubscriberAdder> logger)
        {
            _subscribeRepository = subscribeRepository;
            _logger = logger;
        }
        public async Task<Subscriber> AddSubscriber(Subscriber? subscriber)
        {
            if(subscriber is null)
            {
                throw new ArgumentNullException(nameof(subscriber));
            }

            ValidatorHelper.ModelValidation(subscriber);
            await _subscribeRepository.AddSubscriber(subscriber);

            return subscriber;
        }
    }
}
