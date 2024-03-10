using Entities.IdentityEntities;
using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;
using System;

namespace Services
{
    public class UserGetter : IUserGetterService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<UserGetter> _logger;

        public UserGetter(IUsersRepository usersRepository, ILogger<UserGetter> logger)
        {
            _logger = logger;
            _usersRepository = usersRepository;
        }
        public async Task<List<ApplicationUser>> GetAllUsers()
        {
            return await _usersRepository.GetAllUsers();
        }

        public async Task<ApplicationUser?> GetUserByID(Guid id)
        {
            return await _usersRepository.GetUserByID(id);
        }
    }
}
