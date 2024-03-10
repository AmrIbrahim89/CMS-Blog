using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;

namespace Services
{
    public class MediaDeleter : IMediaDeleterService
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<MediaDeleter> _logger;

        public MediaDeleter(IMediaRepository mediaRepository, ILogger<MediaDeleter> logger)
        {
            _logger = logger;
            _mediaRepository = mediaRepository;
        }
        public async Task<bool> DeleteMediaFile(Guid id)
        {
            return await _mediaRepository.DeleteMedia(id);
        }
    }
}
