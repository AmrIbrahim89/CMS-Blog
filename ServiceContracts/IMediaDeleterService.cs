using System;
namespace ServiceContracts
{
    public interface IMediaDeleterService
    {
        public Task<bool> DeleteMediaFile(Guid id);
    }
}
