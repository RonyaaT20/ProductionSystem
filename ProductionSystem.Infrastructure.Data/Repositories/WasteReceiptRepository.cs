using Microsoft.EntityFrameworkCore;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Infrastructure.Data.Context;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductionSystem.Domain.IRepositories;

namespace ProductionSystem.Infrastructure.Data.Repositories
{
    public class WasteReceiptRepository : GenericRepository<WasteReceipt>, IWasteReceiptRepository
    {
        public WasteReceiptRepository(ProductionSystemDbContext context) : base(context) { }

        public override async Task<List<WasteReceipt>> GetAllAsync()
        {
            return await _dbSet
                .Include(r => r.Order)
                .Include(r => r.Product)
                .Include(r => r.ProductParameter)
                .Include(r => r.ParameterValue)
                .Include(r => r.Personnel)
                    .ThenInclude(p => p.Personnel)
                .ToListAsync();
        }
    }
}