using ProductionSystem.Domain.Entities;
using System.Threading.Tasks;

namespace ProductionSystem.Domain.IRepositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<bool> IsCodeExistsAsync(string code);
        Task<bool> IsCodeExistsAsync(string code, int excludeId);

    }
}
