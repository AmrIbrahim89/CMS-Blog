using Entities.IdentityEntities;

namespace ServiceContracts
{
    public interface IUserGetterService
    {
        public Task<List<ApplicationUser>> GetAllUsers();

        public Task<ApplicationUser?> GetUserByID(Guid id);
    }
}
