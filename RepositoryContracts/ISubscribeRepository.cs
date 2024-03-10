using Entities;

namespace RepositoryContracts
{
    public interface ISubscribeRepository
    {
        public Task<Subscriber> AddSubscriber(Subscriber subscriberDetails);
    }
}
