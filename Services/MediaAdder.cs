using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;

namespace Services
{
    public class MediaAdder : IMediaAdderService
    {
        private readonly IMediaRepository _mediaRepository;
        private readonly ILogger<MediaAdder> _logger;

        public MediaAdder(IMediaRepository mediaRepository, ILogger<MediaAdder> logger)
        {
            _mediaRepository = mediaRepository;
            _logger = logger;
        }
        public async Task<Media> UploadMediaFiles(IFormFile? file)
        {
            if(file is null)
            {
                throw new ArgumentNullException("Image not found.");
            }

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var data = stream.ToArray();

                Media media = new();
                media.Image = data;
                media.ImageName = file.FileName;
                media.ImageSize = (file.Length / 1000).ToString();
                media.ID = Guid.NewGuid();

                return await _mediaRepository.UploadMedia(media);
            }
        }
    }
}
