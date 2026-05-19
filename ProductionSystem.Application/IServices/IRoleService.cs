using ProductionSystem.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionSystem.Application.IServices
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllAsync();
        Task<RoleDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateRoleDto dto);
        Task<bool> EditAsync(EditRoleDto dto);
        Task<DeleteResult> DeleteAsync(int id);
    }
}