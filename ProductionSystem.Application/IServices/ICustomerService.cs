using ProductionSystem.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionSystem.Application.IServices
{
    public interface ICustomerService
    {
        Task<List<CustomerDto>> GetAllAsync();
        Task<CustomerDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateCustomerDto dto);
        Task<bool> EditAsync(EditCustomerDto dto);
        Task<DeleteResult> DeleteAsync(int id);
        Task<bool> IsCodeUniqueAsync(string code, int id = 0);

    }
}