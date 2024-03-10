using Entities;
using Entities.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RepositoryContracts;

namespace Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogger<UsersRepository> _logger;

        public UsersRepository(ApplicationDBContext context, ILogger<UsersRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<ApplicationUser?> GetUserByID(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id) ?? null;
        }

        public async Task<bool> UpdateUser(ApplicationUser user)
        {           
            _context.Users.Update(user);
            int rowsEffected = await _context.SaveChangesAsync();

            return rowsEffected > 0;
        }
    }
}
