using System;

namespace ServiceContracts
{
    public interface IBlogPostDeleterService
    {
        public Task<bool> DeletePost(Guid id);      
        
    }
}
