using Entities.IdentityEntities;

namespace ServiceContracts
{
    public interface IUserUpdaterService
    {
        public Task<bool> UpdateUserDetails(ApplicationUser? user);
    }
}
