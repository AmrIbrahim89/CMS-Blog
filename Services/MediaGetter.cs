using Entities;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;

namespace Services
{
    public class MediaGetter : IMediaGetterService
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<MediaGetter> _logger;
        public MediaGetter(IMediaRepository mediaRepository, ILogger<MediaGetter> logger)
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
        }
        public async Task<List<Media>> GetAllMedia()
        {
           return await _mediaRepository.GetAllMedia();
        }

        public async Task<Media?> GetMediaByID(Guid id)
        {
            return await _mediaRepository.GetMediaByID(id);
        }
    }
}
