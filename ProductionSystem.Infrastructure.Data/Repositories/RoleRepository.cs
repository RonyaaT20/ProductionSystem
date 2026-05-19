using Microsoft.EntityFrameworkCore;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Infrastructure.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductionSystem.Domain.IRepositories;

namespace ProductionSystem.Infrastructure.Data.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ProductionSystemDbContext context) : base(context) { }

        public override async Task<List<Role>> GetAllAsync()
        {
            return await _dbSet
                .Include(r => r.RolePermissions)
                .ToListAsync();
        }

        public override async Task<Role> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(r => r.RolePermissions)
                .FirstOrDefaultAsync(r => r.Id == id);
        }
    }
}