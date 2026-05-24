using ProductionSystem.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionSystem.Application.IServices
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateProductDto dto);
        Task<bool> EditAsync(EditProductDto dto);
        Task<DeleteResult> DeleteAsync(int id);
        Task<bool> IsCodeUniqueAsync(string code, int id = 0);

    }
}