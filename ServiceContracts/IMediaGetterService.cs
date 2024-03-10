using Entities;

namespace ServiceContracts
{
    public interface IMediaGetterService
    {
        public Task<List<Media>> GetAllMedia();
        public Task<Media?> GetMediaByID(Guid id);
    }
}
