using System.Collections.Generic;
using System.Threading.Tasks;
using ProductionSystem.Domain.Entities;

namespace ProductionSystem.Domain.IRepositories
{
    public interface IProductionReceiptRepository : IGenericRepository<ProductionReceipt>
    {
        Task<string> GenerateUniqueBarcodeAsync();
        Task<List<ProductionReceiptParameter>> AddRangeParameter(List<ProductionReceiptParameter> models);
    }
}
