using Entities.IdentityEntities;

namespace RepositoryContracts
{
    public interface IUsersRepository
    {
        public Task<List<ApplicationUser>> GetAllUsers();
        public Task<ApplicationUser?> GetUserByID(Guid id);
        public Task<bool> UpdateUser(ApplicationUser user); 
    }
}
