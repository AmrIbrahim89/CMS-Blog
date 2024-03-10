using Entities;
using Microsoft.AspNetCore.Http;

namespace RepositoryContracts
{
    public interface IMediaRepository
    {
        public Task<Media> UploadMedia(Media media);
        public Task<List<Media>> GetAllMedia();
        public Task<Media?> GetMediaByID(Guid ID);
        public Task<bool> DeleteMedia(Guid ID);
    }
}
