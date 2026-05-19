using Microsoft.EntityFrameworkCore;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Infrastructure.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductionSystem.Domain.IRepositories;

namespace ProductionSystem.Infrastructure.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ProductionSystemDbContext context) : base(context) { }

        public override async Task<List<Product>> GetAllAsync()
        {
            return await _dbSet
                .Include(p => p.Unit)
                .Include(p => p.Parameters)
                .ThenInclude(pp => pp.ProductParameter)
                .Include(p => p.Parameters)
                .ThenInclude(pp => pp.ParameterValue)
                .ToListAsync();
        }

        public override async Task<Product> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(p => p.Unit)
                .Include(p => p.Parameters)
                .ThenInclude(pp => pp.ProductParameter)
                .ThenInclude(pp => pp.Values)
                .Include(p => p.Parameters)
                .ThenInclude(pp => pp.ParameterValue)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}