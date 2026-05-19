using ProductionSystem.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionSystem.Application.IServices
{
    public interface IUnitService
    {
        Task<List<UnitDto>> GetAllAsync();
        Task<UnitDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateUnitDto dto, string createdBy);
        Task<bool> EditAsync(EditUnitDto dto);
        Task<DeleteResult> DeleteAsync(int id);
    }
}