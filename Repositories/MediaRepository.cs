using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryContracts;

namespace Repositories
{
    public class MediaRepository : IMediaRepository
    {
        private readonly ApplicationDBContext _dBcontext;
        private readonly ILogger<MediaRepository> _logger;

        public MediaRepository(ApplicationDBContext dBContext, ILogger<MediaRepository> logger)
        {
            _dBcontext = dBContext;
            _logger = logger;
        }

        public async Task<bool> DeleteMedia(Guid Id)
        {
            _dBcontext.Media.RemoveRange(_dBcontext.Media.Where(media => media.ID == Id));
            int rowsAffected = await _dBcontext.SaveChangesAsync();

            return rowsAffected > 0;
        }

        public async Task<List<Media>> GetAllMedia()
        {
            return await _dBcontext.Media.ToListAsync();
        }

        public async Task<Media?> GetMediaByID(Guid ID)
        {
            return await _dBcontext.Media.FindAsync(ID);
        }

        public async Task<Media> UploadMedia(Media media)
        {
            _dBcontext.Add(media);
            await _dBcontext.SaveChangesAsync();
            return media;
        }
    }
}
