using ProductionSystem.Domain.Entities;
using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Infrastructure.Data.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductionSystem.Infrastructure.Data.Repositories
{
    public class PersonnelRepository : GenericRepository<Personnel>, IPersonnelRepository
    {
        public PersonnelRepository(ProductionSystemDbContext context) : base(context) { }

        private readonly ProductionSystemDbContext _context;

        public async Task<bool> IsCodeExistsAsync(string code)
        {
            return await _context.Personnels.AnyAsync(p => p.Code == code);
        }

        public async Task<bool> IsCodeExistsAsync(string code, int excludeId)
        {
            return await _context.Personnels.AnyAsync(p => p.Code == code && p.Id != excludeId);
        }
    }
}
