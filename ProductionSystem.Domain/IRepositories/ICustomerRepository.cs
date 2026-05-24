using ProductionSystem.Domain.Entities;
using System.Threading.Tasks;

namespace ProductionSystem.Domain.IRepositories
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Task<bool> IsCodeExistsAsync(string code);
        Task<bool> IsCodeExistsAsync(string code, int excludeId);
    }
}
