using Microsoft.EntityFrameworkCore;
using ProductionSystem.Domain.Entities;
using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Infrastructure.Data.Context;
using System.Threading.Tasks;

namespace ProductionSystem.Infrastructure.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ProductionSystemDbContext context) : base(context) { }

        public async Task<bool> IsCodeExistsAsync(string code)
        {
            return await _context.Customers.AnyAsync(c => c.Code == code);
        }

        public async Task<bool> IsCodeExistsAsync(string code, int excludeId)
        {
            return await _context.Customers.AnyAsync(c => c.Code == code && c.Id != excludeId);
        }
    }
}
