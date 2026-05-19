using Microsoft.EntityFrameworkCore;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductionSystem.Domain.IRepositories;

namespace ProductionSystem.Infrastructure.Data.Repositories
{
    public class ProductionReceiptRepository : GenericRepository<ProductionReceipt>, IProductionReceiptRepository
    {
        public ProductionReceiptRepository(ProductionSystemDbContext context) : base(context) { }

        public override async Task<List<ProductionReceipt>> GetAllAsync()
        {
            return await _dbSet
                .Include(r => r.Order)
                .Include(r => r.Product)
                .Include(r => r.ProductReceiptParameters).ThenInclude(r => r.ProductParameter)
                .Include(r => r.ProductReceiptParameters).ThenInclude(r => r.ParameterValue)
                .Include(r => r.Personnel)
                    .ThenInclude(p => p.Personnel)
                .ToListAsync();
        }

        public override async Task<ProductionReceipt> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(r => r.Order)
                .Include(r => r.Product)
                    .ThenInclude(p => p.Unit)
                .Include(r => r.ProductReceiptParameters).ThenInclude(r => r.ProductParameter)
                .Include(r => r.ProductReceiptParameters).ThenInclude(r => r.ParameterValue).Include(r => r.Personnel)
                    .ThenInclude(p => p.Personnel)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<string> GenerateUniqueBarcodeAsync()
        {
            var random = new Random();
            string barcode;
            bool exists;
            do
            {
                barcode = random.Next(1000000000, int.MaxValue).ToString();
                exists = await _dbSet.AnyAsync(p => p.Barcode == barcode);
            } while (exists);
            return barcode;
        }

        public async Task<List<ProductionReceiptParameter>> AddRangeParameter(List<ProductionReceiptParameter> models)
        {
            try
            {
                await _context.AddRangeAsync(models);
                return models;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}