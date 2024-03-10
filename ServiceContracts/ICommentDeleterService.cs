using System;

namespace ServiceContracts
{
    public interface ICommentDeleterService
    {
        public Task<bool> DeleteComment(Guid id);
    }
}
