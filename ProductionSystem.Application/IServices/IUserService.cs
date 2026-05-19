using ProductionSystem.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionSystem.Application.IServices
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateUserDto dto);
        Task<bool> EditAsync(EditUserDto dto);
        Task<DeleteResult> DeleteAsync(int id);
        Task<List<RoleDto>> GetRolesAsync();

    }
}