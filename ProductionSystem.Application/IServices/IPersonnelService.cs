using ProductionSystem.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionSystem.Application.IServices
{
    public interface IPersonnelService
    {
        Task<List<PersonnelDto>> GetAllAsync();
        Task<PersonnelDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreatePersonnelDto dto);
        Task<bool> EditAsync(EditPersonnelDto dto);
        Task<DeleteResult> DeleteAsync(int id);

        Task<bool> IsCodeUniqueAsync(string code, int id = 0);
    }
}