using ProductionSystem.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionSystem.Application.IServices
{
    public interface IParameterService
    {
        Task<List<ParameterDto>> GetAllAsync();
        Task<ParameterDto> GetByIdAsync(int id);
        Task<bool> CreateAsync(CreateParameterDto dto);
        Task<bool> EditAsync(EditParameterDto dto);
        Task<DeleteResult> DeleteAsync(int id);
        //Task<bool> SaveValuesAsync(int parameterId, List<string> values);
        Task<(bool IsUsed, string Message)> IsValueUsedInSystemAsync(int parameterId, string valueTitle);
        Task<List<string>> GetUsedValuesAsync(int parameterId, List<string> values);
        Task<(bool Success, string Message)> SaveValuesAsync(int parameterId, List<string> values);

        Task<(bool Success, string Message)> AddValueAsync(int parameterId, string value);
        Task<(bool Success, string Message)> DeleteValueAsync(int parameterId, string value);
        Task<(bool Success, string Message)> RestoreValueAsync(int parameterId, string value);
        Task<List<ParameterValueDto>> GetDeletedValuesAsync(int parameterId);
        Task<bool> IsCodeUniqueAsync(string code, int id = 0);

    }
}
