using ProductionSystem.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionSystem.Application.IServices
{
    public interface IOrderService
    {
        Task<List<OrderDto>> GetAllAsync();
        Task<OrderDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateOrderDto dto);
        Task<bool> EditAsync(EditOrderDto dto);
        Task<DeleteResult> DeleteAsync(int id);
        Task<bool> IsCodeUniqueAsync(string code, int id = 0);

    }
}