using ProductionSystem.Domain.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductionSystem.Application.IServices
{
    public interface IWasteReceiptService
    {
        Task<List<WasteReceiptDto>> GetAllAsync();
        Task<bool> CreateAsync(CreateWasteReceiptDto dto);
        Task<DeleteResult> DeleteAsync(int id);
    }
}