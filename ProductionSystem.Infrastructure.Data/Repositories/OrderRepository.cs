using Microsoft.EntityFrameworkCore;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Infrastructure.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductionSystem.Domain.IRepositories;

namespace ProductionSystem.Infrastructure.Data.Repositories
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(ProductionSystemDbContext context) : base(context) { }

        public override async Task<List<Order>> GetAllAsync()
        {
            return await _dbSet
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .ThenInclude(p => p.Unit)
                .Include(o => o.Product)
                .ThenInclude(p => p.Parameters)
                .ThenInclude(pp => pp.ProductParameter)
                .ToListAsync();
        }

        public override async Task<Order> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(o => o.Customer)
                .Include(o => o.Product)
                .ThenInclude(p => p.Unit)
                .Include(o => o.Product)
                .ThenInclude(p => p.Parameters)
                .ThenInclude(pp => pp.ProductParameter)
                .ThenInclude(pp => pp.Values)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<bool> IsCodeExistsAsync(string code)
        {
            return await _context.Orders.AnyAsync(o => o.Code == code);
        }

        public async Task<bool> IsCodeExistsAsync(string code, int excludeId)
        {
            return await _context.Orders.AnyAsync(o => o.Code == code && o.Id != excludeId);
        }
    }
}