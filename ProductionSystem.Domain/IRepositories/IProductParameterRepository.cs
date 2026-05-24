using ProductionSystem.Domain.Entities;
using System.Threading.Tasks;

namespace ProductionSystem.Domain.IRepositories
{
    public interface IProductParameterRepository : IGenericRepository<ProductParameter>
    {
        Task<bool> IsCodeExistsAsync(string code);
        Task<bool> IsCodeExistsAsync(string code, int excludeId);
    }



}
