using ProductionSystem.Domain.Entities;
using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Infrastructure.Data.Context;

namespace ProductionSystem.Infrastructure.Data.Repositories
{
    public class PersonnelRepository : GenericRepository<Personnel>, IPersonnelRepository
    {
        public PersonnelRepository(ProductionSystemDbContext context) : base(context) { }
    }
}
