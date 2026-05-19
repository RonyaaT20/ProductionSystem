using ProductionSystem.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionSystem.Application.IServices
{
    public interface IProductionReceiptService
    {
        Task<List<ProductionReceiptDto>> GetAllAsync();
        Task<int> CreateAsync(CreateProductionReceiptDto dto);
        Task<DeleteResult> DeleteAsync(int id);
    }
}