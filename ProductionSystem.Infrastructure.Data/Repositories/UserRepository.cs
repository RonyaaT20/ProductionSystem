using Microsoft.EntityFrameworkCore;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Infrastructure.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductionSystem.Domain.IRepositories;

namespace ProductionSystem.Infrastructure.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ProductionSystemDbContext context) : base(context) { }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbSet
                .Include(u => u.Role)
                    .ThenInclude(r => r.RolePermissions)
                .FirstOrDefaultAsync(u => u.Username == username);
        }
        public override async Task<List<User>> GetAllAsync()
        {
            return await _dbSet
                .Include(u => u.Role)
                .ToListAsync();
        }

    }
}