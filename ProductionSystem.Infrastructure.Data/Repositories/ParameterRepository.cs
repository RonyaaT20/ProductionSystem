using Microsoft.EntityFrameworkCore;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Infrastructure.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductionSystem.Domain.IRepositories;

namespace ProductionSystem.Infrastructure.Data.Repositories
{
    public class ProductParameterRepository : GenericRepository<ProductParameter>, IProductParameterRepository
    {
        public ProductParameterRepository(ProductionSystemDbContext context) : base(context) { }

        public override async Task<List<ProductParameter>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.Unit)
                .Include(p => p.Values)
                .ToListAsync();
        }

        public override async Task<ProductParameter> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Unit)
                .Include(p => p.Values)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<bool> IsCodeExistsAsync(string code)
        {
            return await _context.Parameters.AnyAsync(p => p.Code == code);
        }

        public async Task<bool> IsCodeExistsAsync(string code, int excludeId)
        {
            return await _context.Parameters.AnyAsync(p => p.Code == code && p.Id != excludeId);
        }

    }
}