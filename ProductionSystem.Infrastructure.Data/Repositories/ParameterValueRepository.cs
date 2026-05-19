using System;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Infrastructure.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductionSystem.Infrastructure.Data.Repositories
{
    public class ParameterValueRepository:GenericRepository<ParameterValue>, IParameterValueRepository
    {
        public ParameterValueRepository(ProductionSystemDbContext context) : base(context)
        {
        }
        public async Task<List<string>> GetUsedValuesAsync(int parameterId, List<string> values)
        {
            return await _context.ProductParameterItems 
                .Where(x => x.ProductParameterId == parameterId && values.Contains(x.ParameterValue.Title))
                .Select(x => x.ParameterValue.Title)
                .Distinct()
                .ToListAsync();
        }

        public async Task AddRangeAsync(List<ParameterValue> models)
        {
            try
            {
                await _context.AddRangeAsync(models);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
