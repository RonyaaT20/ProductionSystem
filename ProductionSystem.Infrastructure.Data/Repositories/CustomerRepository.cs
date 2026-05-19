using ProductionSystem.Domain.Entities;
using ProductionSystem.Domain.IRepositories;
using ProductionSystem.Infrastructure.Data.Context;

namespace ProductionSystem.Infrastructure.Data.Repositories
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(ProductionSystemDbContext context) : base(context) { }
    }
}
