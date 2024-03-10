using Entities;

namespace ServiceContracts
{
    public interface ISubscriberAdderService
    {
        public Task<Subscriber> AddSubscriber(Subscriber? subscriberDetails);
    }
}
