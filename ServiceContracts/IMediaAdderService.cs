using Entities;
using Microsoft.AspNetCore.Http;

namespace ServiceContracts
{
    public interface IMediaAdderService
    {
        public Task<Media> UploadMediaFiles(IFormFile? file);
    }
}
