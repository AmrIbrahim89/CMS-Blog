using System;

namespace ServiceContracts
{
    public interface IPageDeleterService
    {
        public Task<bool> DeletePage(Guid? id);
    }
}
